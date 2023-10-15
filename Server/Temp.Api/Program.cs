using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.Temp.Api.Services;
using ModularHouse.Server.Temp.Api.Services.Contracts;
using ModularHouse.Server.Temp.Application;
using ModularHouse.Server.Temp.Domain;
using ModularHouse.Server.Temp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAuthApiService, AuthApiService>();

builder.Services
    .ConfigureDomainServices()
    .ConfigureApplicationServices(builder.Configuration)
    .ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
