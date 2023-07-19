using apsys.heartbeat.services.users;
using apsys.heartbeat.webapi.Infraestructure;
using System.Reflection;

IConfiguration configuration;

var builder = WebApplication.CreateBuilder(args);
configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationUserProfileSearcher).GetTypeInfo().Assembly));
//builder.Services.ConfigurePolicy();
builder.Services.ConfigureCors(configuration);
builder.Services.ConfigureUnitOfWork(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors("CorsPolicy");

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    /** Edit AppScope and set your custom application scope name */
    //.RequireAuthorization("AppScope");
});

app.Run();
