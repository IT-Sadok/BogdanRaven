using LibraryApp.Entities;

namespace LibraryApp.Models;

public record BookModel
{
    public string Id { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public ItemStatus Status { get; init; } = ItemStatus.Available;
    public QualityStatus QualityStatus { get; init; } = QualityStatus.New;
}