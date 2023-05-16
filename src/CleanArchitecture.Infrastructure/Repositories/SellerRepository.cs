using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly CleanArchitecturesDbContext _dbContext;

    public SellerRepository(CleanArchitecturesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Seller>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Sellers.ToListAsync(cancellationToken);
    }

    public Task<Seller?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Sellers.FirstOrDefaultAsync(seller => seller.Id == id, cancellationToken);
    }
}