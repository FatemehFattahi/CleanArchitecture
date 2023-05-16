using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Domain.Models;

public class ProductPrice : BaseEntity
{
    // For Ef Compatibility
    private ProductPrice()
    {
    }

    public ProductPrice(int productId, int sellerId, decimal price, DateOnly priceForDate)
    {
        UpdatePrice(price);
        ProductId = productId;
        SellerId = sellerId;
        PriceForDate = priceForDate;
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new PriceShouldBeGreaterThanZeroException();
        }

        Price = price;
    }

    public int Id { get; set; }

    public int ProductId { get; init; }

    public int SellerId { get; init; }

    public decimal Price { get; private set; }

    public DateOnly PriceForDate { get; init; }

    // Navigation Properties

    public Product Product { get; init; } = default!;

    public Seller Seller { get; init; } = default!;
}