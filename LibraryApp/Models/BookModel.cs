using LibraryApp.Entities;

namespace LibraryApp.Models;

public record BookModel
{
    public string Id { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public LibraryItemStatus Status { get; init; } = LibraryItemStatus.Available;
    public ItemQualityStatus ItemQualityStatus { get; init; } = ItemQualityStatus.New;
}