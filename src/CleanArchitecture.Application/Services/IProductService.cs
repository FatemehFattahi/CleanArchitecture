using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductDto> AddAsync(AddProductRequestDto request, CancellationToken cancellationToken);
    Task<ProductDto> UpdateAsync(UpdateProductRequestDto request, int productId, CancellationToken cancellationToken);
}