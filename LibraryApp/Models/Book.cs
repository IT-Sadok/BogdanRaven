namespace LibraryApp.Models;

public class Book : LibraryItem
{
    public string Author { get; set; }

    public Book()
    {
        
    }

    public Book(string id, string title, int year, string author) : base(id, title, year)
    {
        Author = author;
    }

    public override string ToString() => 
        $"[{Id}] {Title} by {Author}, {Year} - {Status}";
}