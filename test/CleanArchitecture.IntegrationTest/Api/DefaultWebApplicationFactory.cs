using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Infrastructure.Database;
using Xunit.Abstractions;

namespace CleanArchitecture.IntegrationTest.Api;

public class DefaultWebApplicationFactory : WebApplicationFactory<Program>, ITestOutputHelperAccessor
{
    public ITestOutputHelper? OutputHelper { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureLogging(logging => logging
            .ClearProviders()
            .SetMinimumLevel(LogLevel.Error)
            .AddFilter(logLevel => logLevel >= LogLevel.Error)
            .AddConsole()
            .AddXUnit(this));

        builder.ConfigureTestServices(services =>
        {
            var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            inMemorySqlite.Open();
            
            services.RemoveAll(typeof(DbContextOptions<CleanArchitecturesDbContext>));
            services.AddDbContextPool<CleanArchitecturesDbContext>(opt =>
            {
                opt.UseSqlite(inMemorySqlite);
            });
        });
    }
}