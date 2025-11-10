using System.Collections.Concurrent;
using LibraryApp.Entities;

namespace LibraryApp.Services;

public class LibraryState
{
    public ConcurrentDictionary<string, Book> Books { get; set; } = new();
}