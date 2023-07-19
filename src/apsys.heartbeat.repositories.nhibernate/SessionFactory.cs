using apsys.heartbeat.repositories.nhibernate.Mappers;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace apsys.heartbeat.repositories.nhibernate
{
    /// <summary>
    /// Session factory
    /// </summary>
    public class SessionFactory
    {
        // Change connection string name
        public static string ConnectionStringName { get => "AppDbConnectionString"; }
        private readonly IConfiguration configuration;

        /// <summary>
        /// Create the NHibernate Session Factory
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This method search the current-static-session key in the configuration file. If the key is not, then thread_static values is used as default
        /// </remarks>
        public ISessionFactory BuildNHibernateSessionFactory()
        {
            string currentStaticSession = this.configuration["current-static-session"];
            if (string.IsNullOrEmpty(currentStaticSession))
                currentStaticSession = "thread_static";
            return BuildNHibernateSessionFactory(currentStaticSession);
        }

        /// <summary>
        /// Create the NHibernate Session Factory
        /// </summary>
        /// <param name="currentSessionContext"></param>
        /// <returns></returns>
        public ISessionFactory BuildNHibernateSessionFactory(string currentSessionContext)
        {
            string connectionString = GetConnectionStringSettings();
            if (string.IsNullOrEmpty(connectionString))
                throw new System.Configuration.ConfigurationErrorsException($"No [{connectionString}] connection string key found in the configuration file");

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(ApplicationUserMapper).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Validate;
            });
            cfg.AddMapping(domainMapping);
            return cfg.BuildSessionFactory();
        }

        /// <summary>
        /// Get the connection string settings based in the environment
        /// </summary>
        /// <returns></returns>
        public string GetConnectionStringSettings()
            => this.configuration.GetConnectionString(ConnectionStringName);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public SessionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
