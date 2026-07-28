using inventory.Api.Data.Configurations;
using inventory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace inventory.Api.Data;

public class inventoryContext(DbContextOptions<inventoryContext> options)
    : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemEntityConfiguration).Assembly);
    }
}
