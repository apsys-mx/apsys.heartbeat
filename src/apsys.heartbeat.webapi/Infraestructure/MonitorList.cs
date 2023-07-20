namespace apsys.heartbeat.webapi.Infraestructure
{

    /// <summary>
    /// Monitor list singleton
    /// </summary>
    public class MonitorList: List<MonitorService>
    {
        private static readonly Lazy<MonitorList> lazy = new Lazy<MonitorList>(() => new MonitorList());

        public static MonitorList Instance { get { return lazy.Value; } }

        private MonitorList()
        {
        }
    }
}
