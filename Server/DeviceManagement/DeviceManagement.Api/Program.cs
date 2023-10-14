using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ModularHouse.Server.DeviceManagement.Api;
using ModularHouse.Server.DeviceManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureApiServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();