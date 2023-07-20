using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apsys.heartbeat
{
    public class MonitorService
    {
        public string Id { get; set; }
        public double IntervalMinutes { get; set; }


        public MonitorService() 
        { 
            this.Id = Guid.NewGuid().ToString();
        }

        public MonitorService(double intervalMinutes)
        {
            this.Id = Guid.NewGuid().ToString();
            this.IntervalMinutes = intervalMinutes;
        }
    }
}
