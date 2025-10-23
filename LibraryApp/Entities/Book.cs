namespace LibraryApp.Entities;

public record Book : LibraryItem
{
    public string Author { get; init; } = string.Empty;
    public QualityStatus QualityStatus { get; init; } = QualityStatus.New;
    public int BorrowCount { get; init; }
}