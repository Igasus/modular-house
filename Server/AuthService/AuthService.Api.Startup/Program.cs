using Microsoft.AspNetCore.Builder;
using ModularHouse.Server.AuthService.Api.Http;
using ModularHouse.Server.AuthService.Application;
using ModularHouse.Server.AuthService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureHttpWebAppServices()
    .ConfigureInfrastructureServices(builder.Configuration)
    .ConfigureApplicationServices();

var app = builder.Build();

app.ConfigureHttpWebApp();

app.Run();