using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Repositories;

public interface IProductPriceRepository
{
    Task<List<ProductPrice>> GetAllByProductIdAsync(int productId, CancellationToken cancellationToken);

    Task<ProductPrice?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ProductPrice> AddAsync(ProductPrice productPrice, CancellationToken cancellationToken);

    Task<ProductPrice> UpdateAsync(ProductPrice productPrice, CancellationToken cancellationToken);

    Task DeleteAsync(ProductPrice productPrice, CancellationToken cancellationToken);

    Task<bool> IsPriceDefinedForDateAsync(int productId, int sellerId, DateOnly dateTime,
        CancellationToken cancellationToken);
}