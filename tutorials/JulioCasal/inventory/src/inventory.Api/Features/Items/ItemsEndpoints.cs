using inventory.Api.Data;
using inventory.Api.Features.Items.CreateItem;
using inventory.Api.Features.Items.DeleteItem;
using inventory.Api.Features.Items.GetItem;
using inventory.Api.Features.Items.GetItems;
using inventory.Api.Features.Items.UpdateItem;
using inventory.Api.Models;

namespace inventory.Api.Features.Items;

public static class ItemsEndpoints
{
    public static void MapItems(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/items");

        group.MapGetItems();
        group.MapGetItem();
        group.MapCreateItem();
        group.MapUpdateItem();
        group.MapDeleteItem();
    }
}
