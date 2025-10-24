namespace LibraryApp.Entities;

public record class LibraryItem
{
    public string InternalId { get; } = Guid.NewGuid().ToString();
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public LibraryItemStatus Status { get; init; } = LibraryItemStatus.Available;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}