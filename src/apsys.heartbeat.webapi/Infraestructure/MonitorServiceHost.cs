using apsys.heartbeat.repositories;
using apsys.heartbeat.repositories.nhibernate;

namespace apsys.heartbeat.webapi.Infraestructure
{
    public class MonitorServiceHost : BackgroundService
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<MonitorServiceHost> logger;
        private readonly MonitorService service;

        public MonitorServiceHost(ILoggerFactory loggerFactory, MonitorService service)
        {
            this.loggerFactory = loggerFactory;
            this.service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new (TimeSpan.FromSeconds(5));
            while ( await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested) 
            {
                
                Console.WriteLine("Entry");
            }
        }

    }
}
