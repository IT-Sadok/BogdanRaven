using LibraryApp.Models;

namespace LibraryApp.Services;

public class LibraryState
{
    public List<Book> Books { get; set; } = new();
}