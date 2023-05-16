using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Repositories;

public interface ISellerRepository
{
    Task<List<Seller>> GetAllAsync(CancellationToken cancellationToken);

    Task<Seller?> GetByIdAsync(int id, CancellationToken cancellationToken);
}