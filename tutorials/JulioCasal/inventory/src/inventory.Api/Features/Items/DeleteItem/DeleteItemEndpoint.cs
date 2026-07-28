using inventory.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Api.Features.Items.DeleteItem;

public static class DeleteItemEndpoint
{
    public static void MapDeleteItem(this IEndpointRouteBuilder app)
    {
        // DELETE /items/122233-434d-43434....
        app.MapDelete("/{id}", async (Guid id, inventoryContext dbContext) =>
        {
            await dbContext.Items
                     .Where(item => item.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
