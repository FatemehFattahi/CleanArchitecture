using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Database.Extensions;

namespace CleanArchitecture.Infrastructure.Database;

public class CleanArchitecturesDbContext : DbContext
{
    public CleanArchitecturesDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<ProductPrice> ProductPrices { get; set; } = default!;

    public DbSet<Seller> Sellers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchitecturesDbContext).Assembly);
       
        modelBuilder.Seed();
    }

    public override int SaveChanges()
    {
        UpdateCreationAndUpdateDateTime();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateCreationAndUpdateDateTime();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        UpdateCreationAndUpdateDateTime();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateCreationAndUpdateDateTime()
    {
        // update CreationDateTime or UpdateDateTime
        var entries = ChangeTracker
            .Entries()
            .Where(e => 
                e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.Now;
            }
            else
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.Now;
            }
        }
    }
}