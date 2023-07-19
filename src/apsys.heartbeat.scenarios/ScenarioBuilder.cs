using apsys.heartbeat.repositories;
using apsys.heartbeat.repositories.nhibernate;
using apsys.ndbunit.netcore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NHibernate;
using System.Data;
using System.Reflection;

namespace apsys.heartbeat.scenarios
{
    /// <summary>
    /// Scenario builder class
    /// </summary>
    public class ScenarioBuilder
    {
        private readonly Assembly[] Assemblies;
        protected internal ServiceProvider _serviceProvider;

        protected internal IUnitOfWork unitOfWork;
        protected internal SessionFactory sessionFactory;
        protected internal IConfiguration configuration;
        protected internal INDbUnit NDbUnitTest;


        /// <summary>
        /// Constructor
        /// </summary>
        public ScenarioBuilder()
        {
            this.Assemblies = new List<Assembly> { typeof(IScenario).Assembly }.ToArray();

            string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            this.sessionFactory = new SessionFactory(this.configuration);
            string connectionString = this.sessionFactory.GetConnectionStringSettings();
            /** Replace the AppSchema with the name of the scheme selected for your app */
            AppSchema schema = new AppSchema();
            this.NDbUnitTest = new SqlClienteNDbUnit(schema, connectionString);

            var sessionFactory = this.sessionFactory.BuildNHibernateSessionFactory();
            var session = sessionFactory.OpenSession();
            LoggerFactory loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<UnitOfWork>();
            this.unitOfWork = new UnitOfWork(session, logger, configuration);

            _serviceProvider = new ServiceCollection()
                .Scan(scan => scan
                    .FromAssemblyOf<SC001_CreateSandBox>()
                    .AddClasses(classes => classes.AssignableTo<IScenario>())
                    .AsSelf()
                    .WithScopedLifetime()
            )
            .AddLogging()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISession>(sesion => sessionFactory.OpenSession())
            .AddSingleton<INDbUnit>(NDbUnitTest)
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider();
        }

        /// <summary>
        /// Build scenario with the name
        /// </summary>
        /// <param name="scenarioName"></param>
        public IScenario Build(string scenarioName)
        {
            var allScenarios = this.ReadAllScenariosFromAssemblies();
            if (!allScenarios.ContainsKey(scenarioName))
                throw new ArgumentOutOfRangeException($"No scenario found with name $[{scenarioName}]");
            var scenarioToBuild = allScenarios[scenarioName];
            return scenarioToBuild;
        }

        private IDictionary<string, IScenario> ReadAllScenariosFromAssemblies()
        {
            IDictionary<string, IScenario> allScenarios = new Dictionary<string, IScenario>();
            foreach (Assembly assembly in this.Assemblies)
            {
                try
                {
                    var scenarioType = typeof(IScenario);
                    var scenariosTypes = this.Assemblies
                        .SelectMany(s => s.GetTypes())
                        .Where(p => scenarioType.IsAssignableFrom(p));
                    foreach (var scenario in scenariosTypes)
                    {
                        ScenarioNameAttribute? customAttributes = (scenario.GetCustomAttribute(typeof(ScenarioNameAttribute), true)) as ScenarioNameAttribute;
                        if (customAttributes != null)
                        {
                            IScenario? scenarioFound = this._serviceProvider.GetService(scenario) as IScenario;
                            allScenarios.Add(customAttributes.ScenarioName, scenarioFound);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new TypeLoadException($"Error loading scenario from assembly {assembly.FullName}", ex);
                }
            }
            return allScenarios;
        }
    }
}