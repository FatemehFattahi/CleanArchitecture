using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public class ProductPriceRepository : IProductPriceRepository
{
    private readonly CleanArchitecturesDbContext _dbContext;

    public ProductPriceRepository(CleanArchitecturesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductPrice> AddAsync(ProductPrice request, CancellationToken cancellationToken)
    {
        var productPrice = await _dbContext.ProductPrices.AddAsync(request, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return productPrice.Entity;
    }

    public Task<List<ProductPrice>> GetAllByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        return _dbContext.ProductPrices
            .Where(productPrice => productPrice.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsPriceDefinedForDateAsync(int productId, int sellerId, DateOnly dateTime,
        CancellationToken cancellationToken)
    {
        var productPrice = await _dbContext
            .ProductPrices
            .FirstOrDefaultAsync(productPrice =>
                    productPrice.ProductId == productId
                    && productPrice.SellerId == sellerId
                    && productPrice.PriceForDate == dateTime
                , cancellationToken);

        return productPrice is not null;
    }

    public Task<ProductPrice?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.ProductPrices
            .FirstOrDefaultAsync(productPrice => productPrice.Id == id, cancellationToken);
    }

    public async Task<ProductPrice> UpdateAsync(ProductPrice productPrice, CancellationToken cancellationToken)
    {
        _dbContext.Update(productPrice);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return productPrice;
    }

    public async Task DeleteAsync(ProductPrice productPrice, CancellationToken cancellationToken)
    {
        _dbContext.ProductPrices.Remove(productPrice);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}