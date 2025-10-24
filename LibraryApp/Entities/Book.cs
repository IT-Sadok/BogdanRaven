namespace LibraryApp.Entities;

public record Book: LibraryItem
{
    public string Id { get; init; }
    public string Author { get; init; } = string.Empty;
    public ItemQualityStatus ItemQualityStatus { get; init; } = ItemQualityStatus.New;
    public int BorrowCount { get; init; }

    public virtual bool Equals(Book? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return other.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}