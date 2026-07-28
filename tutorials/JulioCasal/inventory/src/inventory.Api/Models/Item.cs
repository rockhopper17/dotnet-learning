using inventory.Api.Models;

namespace inventory.Api.Models;

public class Item
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public Category? Category { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public required string Description { get; set; }

    public required string LastUpdatedBy { get; set; }
}
