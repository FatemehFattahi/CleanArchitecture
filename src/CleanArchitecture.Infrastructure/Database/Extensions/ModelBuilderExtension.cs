using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Infrastructure.Database.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seller>()
            .HasData(new List<Seller>
            {
                new("SellerOne")
                {
                    Id = 1
                },
                new("SellerTwo")
                {
                    Id = 2
                }
            });
    }
}