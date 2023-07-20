using apsys.heartbeat.repositories;
using apsys.heartbeat.repositories.nhibernate;

namespace apsys.heartbeat.webapi.Infraestructure
{
    public class MonitorServiceHost : BackgroundService
    {
        private readonly ILogger<MonitorServiceHost> logger;
        private readonly MonitorService service;

        public MonitorServiceHost(ILogger<MonitorServiceHost> logger, MonitorService service)
        {
            this.logger = logger;
            this.service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new (TimeSpan.FromSeconds(this.service.IntervalMinutes));
            while ( await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested) 
            {
                this.logger.LogInformation($"Service with id [{this.service.Id}] with interval [{this.service.IntervalMinutes}] activated at {DateTime.Now.ToString()}");
            }
        }

    }
}
