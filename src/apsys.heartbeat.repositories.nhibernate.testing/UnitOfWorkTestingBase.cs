using apsys.ndbunit.netcore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace apsys.heartbeat.repositories.nhibernate.testing
{
    public abstract class UnitOfWorkTestingBase
    {

        protected internal IUnitOfWork unitOfWork;
        protected internal SessionFactory sessionFactory;
        protected internal IConfiguration configuration;
        protected internal INDbUnit nDbUnitTest;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            this.configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            this.sessionFactory = new SessionFactory(this.configuration);
            string connectionString = sessionFactory.GetConnectionStringSettings();
            /** Replace the AppSchema with the name of the scheme selected for your application */
            AppSchema schema = new AppSchema();
            this.nDbUnitTest = new SqlClienteNDbUnit(schema, connectionString);
        }

        [SetUp]
        public void Setup()
        {
            var sessionFactory = this.sessionFactory.BuildNHibernateSessionFactory();
            var session = sessionFactory.OpenSession();
            LoggerFactory loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<UnitOfWork>();
            this.unitOfWork = new UnitOfWork(session, logger, configuration);
            this.nDbUnitTest.ClearDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            this.unitOfWork.Dispose();
        }
    }
}