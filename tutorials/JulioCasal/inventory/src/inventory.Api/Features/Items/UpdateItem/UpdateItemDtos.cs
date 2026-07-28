using System.ComponentModel.DataAnnotations;

namespace inventory.Api.Features.Items.UpdateItem;

public record UpdateItemDto(
    [Required][StringLength(50)] string Name,
    Guid CategoryId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);
