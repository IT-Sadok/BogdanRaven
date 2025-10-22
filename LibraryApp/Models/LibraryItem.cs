namespace LibraryApp.Models;

public abstract class LibraryItem
{
    public string Id { get; protected set; }
    public string Title { get; protected set; }
    public int Year { get; protected set; }
    public ItemStatus Status { get; protected set; }

    protected LibraryItem(string id, string title, int year, ItemStatus status = ItemStatus.Available)
    {
        Id = id;
        Title = title;
        Year = year;
        Status = status;
    }

    public virtual void Borrow() => Status = ItemStatus.Borrowed;
    public virtual void Return() => Status = ItemStatus.Available;

    public override string ToString() =>
        $"{Title} ({Year}) - Status: {Status}";
}