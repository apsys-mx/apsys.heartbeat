using apsys.heartbeat.repositories;
using apsys.heartbeat.repositories.nhibernate;
using Microsoft.AspNetCore.Authorization;
using System.Resources;

namespace apsys.heartbeat.webapi.Infraestructure
{
    public static class ServiceCollectionExtender
    {
        /// <summary>
        /// Configure policies
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurePolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                /** Edit AppScope and set your custom application scope name */
                options.AddPolicy("AppScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    //policy.AddRequirements(new ApplicationUserRequirement());
                });
                options.AddPolicy("AdministratorRole", policy =>
                {
                    //policy.AddRequirements(new ApplicationRoleRequirement(Resources.Role_Administrator));
                });
            });

            //services.AddScoped<IAuthorizationHandler, ApplicationRoleHandler>();
            //services.AddScoped<IAuthorizationHandler, ApplicationUserHandler>();
        }

        /// <summary>
        /// Configure CORS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            string[] allowedCorsOrigins = GetAllowedCorsOrigins(configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(allowedCorsOrigins)
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        private static string[] GetAllowedCorsOrigins(IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("CorsConfiguration:AllowedOrigins").Value;
            if (string.IsNullOrEmpty(allowedOrigins))
                throw new ArgumentException($"No CORS configuration found in the configuration file");
            return allowedOrigins.Split(",");
        }

        /// <summary>
        /// Configure the unit of work dependency injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            SessionFactory factory = new SessionFactory(configuration);
            var sessionFactory = factory.BuildNHibernateSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void StartMonitorServices(this IServiceCollection services, IConfiguration configuration)
        {
            SessionFactory builderFactory = new SessionFactory(configuration);
            var sessionFactory = builderFactory.BuildNHibernateSessionFactory();
            var session = sessionFactory.OpenSession();

            IUnitOfWork uow = new UnitOfWork(session, null, configuration);

            foreach (var monitorService in uow.Monitors.GetRegisteded())
                services.AddHostedService(provider => new MonitorServiceHost(null, monitorService));
        }
    }
}
