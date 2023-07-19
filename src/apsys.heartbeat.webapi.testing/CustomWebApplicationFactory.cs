using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace apsys.heartbeat.webapi.testing
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureServices(services =>
            //{
            //    //ScenarioBuilder builder = new ScenarioBuilder();
            //    //var sandbox = builder.Build("CreateSandBox");
            //    //sandbox.SeedData();

            //    //var initialize = builder.Build("InitializeApplication");
            //    //initialize.SeedData();

            //    //var loadDrafts = builder.Build("LoadRequestsDraft");
            //    //loadDrafts.SeedData();
            //});
            builder.UseEnvironment("Development");
        }
    }
}
