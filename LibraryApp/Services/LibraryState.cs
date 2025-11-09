using LibraryApp.Entities;

namespace LibraryApp.Services;

public class LibraryState
{
    public HashSet<Book> Books { get; set; } = new();
}