using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly CleanArchitecturesDbContext _dbContext;

    public ProductRepository(CleanArchitecturesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Products.ToListAsync(cancellationToken);
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public async Task<Product> AddAsync(Product request, CancellationToken cancellationToken)
    {
         var product = await _dbContext.Products.AddAsync(request, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product.Entity;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var updatedProduct = _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return updatedProduct.Entity;
    }
}