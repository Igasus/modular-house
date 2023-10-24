using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.DeviceManagement.Api;
using ModularHouse.Server.DeviceManagement.Domain;
using ModularHouse.Server.DeviceManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration)
    .ConfigureDomainServices();

var app = builder.Build();

app.UseWebApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.Services.InitializeDatabase();

app.Run();