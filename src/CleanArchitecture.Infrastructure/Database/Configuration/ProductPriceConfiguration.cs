using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Database.Convertors;

namespace CleanArchitecture.Infrastructure.Database.Configuration;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.HasKey(productPrice => productPrice.Id);

        builder
            .Property(productPrice => productPrice.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(productPrice => productPrice.Price)
            .HasColumnType("decimalcls(15,3)")
            .IsRequired();

        builder.Property(productPrice => productPrice.PriceForDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .HasColumnType("date");

        builder
            .Property(productPrice => productPrice.CreatedAt)
            .HasColumnType("datetimeoffset");

        builder
            .Property(productPrice => productPrice.UpdatedAt)
            .HasColumnType("datetimeoffset");

        builder
            .HasOne(productPrice => productPrice.Product)
            .WithMany(product => product.ProductPrices)
            .HasForeignKey(productPrice => productPrice.ProductId);

        builder
            .HasOne(productPrice => productPrice.Seller)
            .WithMany(seller => seller.ProductPrices)
            .HasForeignKey(productPrice => productPrice.SellerId);
    }
}