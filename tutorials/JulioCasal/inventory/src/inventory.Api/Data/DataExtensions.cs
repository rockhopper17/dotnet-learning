using Azure.Core;
using inventory.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace inventory.Api.Data;

public static class DataExtensions
{
    public static WebApplicationBuilder AddinventoryNpgsql<TContext>(
        this WebApplicationBuilder builder,
        string connectionStringName,
        TokenCredential credential
    ) where TContext : DbContext
    {
        if (builder.Environment.IsProduction())
        {
            builder.AddAzureNpgsqlDbContext<TContext>(
                connectionStringName,
                settings => settings.Credential = credential,
                configureDbContextOptions: options =>
                    ConfigureDbContext(options)
            );
        }
        else
        {
            builder.AddNpgsqlDbContext<TContext>(
                connectionStringName,
                configureDbContextOptions: options =>
                    ConfigureDbContext(options));
        }

        return builder;
    }

    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        inventoryContext dbContext = scope.ServiceProvider
                                          .GetRequiredService<inventoryContext>();
        await dbContext.Database.MigrateAsync();
    }

    private static DbContextOptionsBuilder ConfigureDbContext(DbContextOptionsBuilder options)
    {
        return options.UseSeeding((context, _) =>
                    {
                        if (!context.Set<Category>().Any())
                        {
                            SeedCategories(context);
                            context.SaveChanges();
                        }

                        if (!context.Set<Item>().Any())
                        {
                            SeedItems(context);
                            context.SaveChanges();
                        }
                    })
                    .UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        if (!context.Set<Category>().Any())
                        {
                            SeedCategories(context);
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        if (!context.Set<Item>().Any())
                        {
                            SeedItems(context);
                            await context.SaveChangesAsync(cancellationToken);
                        }
                    });
    }

    private static void SeedCategories(DbContext context)
    {
        context.Set<Category>().AddRange(
            new Category { Name = "General" },
            new Category { Name = "Urgent" },
            new Category { Name = "Archived" },
            new Category { Name = "Favorites" },
            new Category { Name = "Upcoming" }
        );
    }

    private static void SeedItems(DbContext context)
    {
        var generalCategory = context.Set<Category>().First(c => c.Name == "General");
        var upcomingCategory = context.Set<Category>().First(c => c.Name == "Upcoming");
        var favoritesCategory = context.Set<Category>().First(c => c.Name == "Favorites");

        context.Set<Item>().AddRange(
            new Item
            {
                Name = "Mechanical Keyboard",
                CategoryId = generalCategory.Id,
                Price = 129.99m,
                ReleaseDate = new DateOnly(2025, 1, 15),
                Description = "RGB backlit mechanical keyboard with Cherry MX switches",
                LastUpdatedBy = "system"
            },
            new Item
            {
                Name = "4K Monitor",
                CategoryId = upcomingCategory.Id,
                Price = 449.99m,
                ReleaseDate = new DateOnly(2025, 6, 20),
                Description = "27-inch 4K UHD display with HDR support and 144Hz refresh rate",
                LastUpdatedBy = "system"
            },
            new Item
            {
                Name = "Wireless Mouse",
                CategoryId = favoritesCategory.Id,
                Price = 79.99m,
                ReleaseDate = new DateOnly(2024, 11, 10),
                Description = "Ergonomic wireless mouse with programmable buttons and long battery life",
                LastUpdatedBy = "system"
            }
        );
    }
}
