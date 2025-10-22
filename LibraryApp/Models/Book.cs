using System.Text.Json.Serialization;

namespace LibraryApp.Models;

public class Book : LibraryItem
{
    public string Author { get; private set; }
    
    [JsonConstructor]
    public Book(string id, string title, string author, int year, ItemStatus status)
        : base(id, title, year, status)
    {
        Author = author;
    }

    public Book(string id,string title, string author, int year)
        : base(id, title, year, ItemStatus.Available)
    {
        Author = author;
    }

    public override string ToString() => 
        $"[{Id}] {Title} by {Author}, {Year} - {Status}";
}