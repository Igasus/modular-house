using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.Auth.Api.Http;
using ModularHouse.Server.Auth.Application;
using ModularHouse.Server.Auth.Domain;
using ModularHouse.Server.Auth.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureHttpWebAppServices()
    .ConfigureInfrastructureServices()
    .ConfigureApplicationServices()
    .ConfigureDomainServices();

var app = builder.Build();

app.ConfigureHttpWebApp();

app.Run();