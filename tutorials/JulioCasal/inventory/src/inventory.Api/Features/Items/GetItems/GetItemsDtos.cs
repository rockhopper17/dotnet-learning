namespace inventory.Api.Features.Items.GetItems;

public record GetItemsDto(
    int PageNumber = 1,
    int PageSize = 5,
    string? Name = null);

public record ItemsPageDto(int TotalPages, IEnumerable<ItemSummaryDto> Data);

public record ItemSummaryDto(
    Guid Id,
    string Name,
    string Category,
    decimal Price,
    DateOnly ReleaseDate,
    string LastUpdatedBy
);
