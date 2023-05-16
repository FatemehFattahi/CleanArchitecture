using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEfCore(configuration);
        services.AddRepositories();

        return services;
    }

    private static void AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<CleanArchitecturesDbContext>((sp, options) =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultDatabase"));
            options.EnableSensitiveDataLogging();
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
    }
}