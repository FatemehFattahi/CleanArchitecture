using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Product> AddAsync(Product product, CancellationToken cancellationToken);

    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
}