using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using inventory.Api.Data;
using inventory.Api.Features.Items.Constants;
using inventory.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Api.Features.Items.CreateItem;

public static class CreateItemEndpoint
{
    public static void MapCreateItem(this IEndpointRouteBuilder app)
    {
        // POST /items
        app.MapPost("/", async (
            CreateItemDto itemDto,
            inventoryContext dbContext,
            ILogger<Program> logger,
            ClaimsPrincipal user) =>
        {
            var userEmail = user?.FindFirstValue(JwtRegisteredClaimNames.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Results.Unauthorized();
            }

            var item = new Item
            {
                Name = itemDto.Name,
                CategoryId = itemDto.CategoryId,
                Price = itemDto.Price,
                ReleaseDate = itemDto.ReleaseDate,
                Description = itemDto.Description,
                LastUpdatedBy = userEmail
            };

            dbContext.Items.Add(item);

            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Created item {ItemName} with price {ItemPrice}",
                item.Name,
                item.Price);

            return Results.CreatedAtRoute(
                EndpointNames.GetItem,
                new { id = item.Id },
                new ItemDetailsDto(
                    item.Id,
                    item.Name,
                    item.CategoryId,
                    item.Price,
                    item.ReleaseDate,
                    item.Description,
                    item.LastUpdatedBy
                ));
        })
        .RequireAuthorization()
        .Produces<ItemDetailsDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
