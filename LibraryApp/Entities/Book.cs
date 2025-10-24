namespace LibraryApp.Entities;

public record Book : LibraryItem
{
    public string Author { get; init; } = string.Empty;
    public ItemQualityStatus ItemQualityStatus { get; init; } = ItemQualityStatus.New;
    public int BorrowCount { get; init; }
}