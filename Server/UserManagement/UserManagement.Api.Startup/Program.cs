using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.UserManagement.Api.Http;
using ModularHouse.Server.UserManagement.Application;
using ModularHouse.Server.UserManagement.Domain;
using ModularHouse.Server.UserManagement.Infrastructure;

var webApiApplicationBuilder = WebApplication.CreateBuilder();

webApiApplicationBuilder.Services
    .ConfigureWebApiServices()
    .ConfigureApplicationServices()
    .ConfigureDomainServices()
    .ConfigureInfrastructureServices(webApiApplicationBuilder.Configuration);

var webApiApplication = webApiApplicationBuilder.Build();

webApiApplication.ConfigureWebApi();

webApiApplication.Run();
