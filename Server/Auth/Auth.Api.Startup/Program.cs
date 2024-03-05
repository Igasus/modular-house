using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.Auth.Api.Http;
using ModularHouse.Server.Auth.Application;
using ModularHouse.Server.Auth.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureHttpWebAppServices()
    .ConfigureInfrastructureServices(builder.Configuration)
    .ConfigureApplicationServices();

var app = builder.Build();

app.ConfigureHttpWebApp();

app.Run();