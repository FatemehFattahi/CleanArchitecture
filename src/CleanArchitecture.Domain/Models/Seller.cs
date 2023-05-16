namespace CleanArchitecture.Domain.Models;

public class Seller : BaseEntity
{
    // For Ef Compatibility
    private Seller()
    {
    }

    public Seller(string name)
    {
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; } = default!;

    // Navigation Properties
    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
}