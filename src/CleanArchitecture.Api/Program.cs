using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;

var builder = WebApplication
    .CreateBuilder(args)
    .UseSerilogWithCustomConfig();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
});
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
// Validate AutoMapper profiles
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseGlobalExceptionHandler();
app.AutomaticDatabaseMigratorMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await app.RunAsync(app.Lifetime.ApplicationStopped);

public abstract partial class Program
{
}