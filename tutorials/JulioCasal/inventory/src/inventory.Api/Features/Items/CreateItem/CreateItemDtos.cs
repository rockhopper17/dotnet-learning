using System.ComponentModel.DataAnnotations;

namespace inventory.Api.Features.Items.CreateItem;

public record CreateItemDto(
    [Required][StringLength(50)] string Name,
    Guid CategoryId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record ItemDetailsDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string LastUpdatedBy);