using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Infrastructure.Database.Configuration;

internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder
            .HasKey(seller => seller.Id);

        builder
            .Property(seller => seller.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(seller => seller.Name)
            .HasColumnType("varchar(256)");

        builder
            .Property(seller => seller.CreatedAt)
            .HasColumnType("datetimeoffset");

        builder
            .Property(seller => seller.UpdatedAt)
            .HasColumnType("datetimeoffset");
    }
}