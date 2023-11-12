using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.Common.Api.Middlewares;
using ModularHouse.Server.UserManagement.Application;
using ModularHouse.Server.UserManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureApplicationServices()
    .ConfigureInfrastructureServices();

builder.Services
    .AddTransactionMiddleware()
    .AddExceptionHandlerMiddleware();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseTransactionMiddleware();
app.UseExceptionHandlerMiddleware();

app.Run();