namespace GameStore.Api.Dtos;

// a DTO is a contract between the client and server since it represents a shared agreement
// about how data will be transferred and used
public record GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
