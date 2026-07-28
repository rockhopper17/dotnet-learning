using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using inventory.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Api.Features.Items.UpdateItem;

public static class UpdateItemEndpoint
{
    public static void MapUpdateItem(this IEndpointRouteBuilder app)
    {
        // PUT /items/122233-434d-43434....
        app.MapPut("/{id}", async (
            Guid id,
            UpdateItemDto itemDto,
            inventoryContext dbContext,
            ClaimsPrincipal user) =>
        {
            var userEmail = user?.FindFirstValue(JwtRegisteredClaimNames.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Results.Unauthorized();
            }

            var existingItem = await dbContext.Items.FindAsync(id);

            if (existingItem is null)
            {
                return Results.NotFound();
            }

            existingItem.Name = itemDto.Name;
            existingItem.CategoryId = itemDto.CategoryId;
            existingItem.Price = itemDto.Price;
            existingItem.ReleaseDate = itemDto.ReleaseDate;
            existingItem.Description = itemDto.Description;
            existingItem.LastUpdatedBy = userEmail;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound);
    }
}
