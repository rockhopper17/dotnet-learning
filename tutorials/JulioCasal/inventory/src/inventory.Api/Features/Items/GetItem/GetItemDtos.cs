namespace inventory.Api.Features.Items.GetItem;

public record ItemDetailsDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string LastUpdatedBy);
