using inventory.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Api.Features.Items.GetItems;

public static class GetItemsEndpoint
{
    public static void MapGetItems(this IEndpointRouteBuilder app)
    {
        // GET /items
        app.MapGet("/", async (
            inventoryContext dbContext,
            [AsParameters] GetItemsDto request) =>
        {
            var skipCount = (request.PageNumber - 1) * request.PageSize;

            var filteredItems = dbContext.Items
                                .Where(item => string.IsNullOrWhiteSpace(request.Name)
                                        || EF.Functions.Like(item.Name, $"%{request.Name}%"));

            var itemsOnPage = await filteredItems
                                .OrderBy(item => item.Name)
                                .Skip(skipCount)
                                .Take(request.PageSize)
                                .Include(item => item.Category)
                                .Select(item => new ItemSummaryDto(
                                    item.Id,
                                    item.Name,
                                    item.Category!.Name,
                                    item.Price,
                                    item.ReleaseDate,
                                    item.LastUpdatedBy
                                ))
                                .AsNoTracking()
                                .ToListAsync();

            var totalItems = await filteredItems.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

            return new ItemsPageDto(totalPages, itemsOnPage);
        })
        .Produces<ItemsPageDto>();
    }
}
