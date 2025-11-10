using System.Collections.Concurrent;
using LibraryApp.Entities;

namespace LibraryApp.Services;

public class LibraryState
{
    public Dictionary<string, Book> Books { get; set; } = new();
}