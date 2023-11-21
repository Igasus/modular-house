using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.DeviceManagement.Api.Http;
using ModularHouse.Server.DeviceManagement.Application;
using ModularHouse.Server.DeviceManagement.Domain;
using ModularHouse.Server.DeviceManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration)
    .ConfigureApplicationServices()
    .ConfigureDomainServices();

var app = builder.Build();

app.ConfigureWebApi();
app.Services.InitializeDatabase();

app.Run();