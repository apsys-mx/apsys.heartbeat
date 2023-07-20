using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apsys.heartbeat
{
    public class MonitorService
    {

        public double IntervalMinutes { get; set; }

        public async Task Start()
        {
            PeriodicTimer timer = new(TimeSpan.FromMilliseconds(this.IntervalMinutes));
            while (await timer.WaitForNextTickAsync()){

            }
        }
    }
}
