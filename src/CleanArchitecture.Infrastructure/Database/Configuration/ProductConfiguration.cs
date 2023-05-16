using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Infrastructure.Database.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder
            .Property(product => product.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(product => product.Ean)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder
            .Property(product => product.Sku)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder
            .Property(product => product.Name)
            .HasColumnType("varchar(512)")
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasColumnType("varchar(2048)")
            .IsRequired();

        builder
            .Property(product => product.CreatedAt)
            .HasColumnType("datetimeoffset");

        builder
            .Property(product => product.UpdatedAt)
            .HasColumnType("datetimeoffset");
    }
}