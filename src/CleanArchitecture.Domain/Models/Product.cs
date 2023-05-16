
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Domain.Models;

public class Product : BaseEntity
{
    // For EF compatibility
    private Product()
    {
    }

    public Product(string ean, string sku, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(ean))
        {
            throw new ArgumentNullOrEmptyException(nameof(ean));
        }

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentNullOrEmptyException(nameof(sku));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullOrEmptyException(nameof(name));
        }

        Ean = ean;
        Sku = sku;
        Name = name;
        Description = description;
    }

    public int Id { get; init; } = default!;

    public string Ean { get; init; } = default!;

    public string Sku { get; init; } = default!;

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    // Navigation Properties
    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
}