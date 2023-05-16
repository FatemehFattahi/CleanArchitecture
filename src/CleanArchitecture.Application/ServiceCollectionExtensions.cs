using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.MappingProfiles;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductPriceService, ProductPriceService>();
        services.AddScoped<ISellerService, SellerService>();

        services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

        return services;
    }
}