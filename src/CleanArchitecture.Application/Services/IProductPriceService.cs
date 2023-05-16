using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Services;

public interface IProductPriceService
{
    Task<List<ProductPriceDto>> GetAllPricesForProductAsync(int productId,
        CancellationToken cancellationToken);

    Task<ProductPriceDto> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ProductPriceDto> AddProductPriceForSellerAsync(AddProductPriceRequestDto request,
        CancellationToken cancellationToken);

    Task<ProductPriceDto> UpdateAsync(UpdateProductPriceRequestDto request,
        CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}