namespace LibraryApp.Models;

public record LibraryItem
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public ItemStatus Status { get; init; } = ItemStatus.Available;
}