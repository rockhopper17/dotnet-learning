using inventory.Api.Features.Categories.GetCategories;

namespace inventory.Api.Features.Categories;

public static class CategoriesEndpoints
{
    public static void MapCategories(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories");

        group.MapGetCategories();
    }
}
