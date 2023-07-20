using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apsys.heartbeat.repositories
{
    public interface IMonitorServiceRepository : IRepository<MonitorService>
    {
        IEnumerable<MonitorService> GetRegisteded();
    }
}
