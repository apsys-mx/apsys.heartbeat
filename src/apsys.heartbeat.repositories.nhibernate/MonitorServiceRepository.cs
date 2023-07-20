using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apsys.heartbeat.repositories.nhibernate
{
    public class MonitorServiceRepository : Repository<MonitorService>, IMonitorServiceRepository
    {
        public MonitorServiceRepository(ISession session) 
            : base(session)
        {
        }

        public IEnumerable<MonitorService> GetRegisteded()
        {
            var list = new List<MonitorService>
            {
                new MonitorService()
            };
            return list;
        }
    }
}
