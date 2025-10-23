namespace LibraryApp.Entities;

public record LibraryItem
{
    public string InternalId { get; } = Guid.NewGuid().ToString();
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public ItemStatus Status { get; init; } = ItemStatus.Available;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}