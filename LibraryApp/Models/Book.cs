namespace LibraryApp.Models;

public record Book : LibraryItem
{
    public string Author { get; init; } = string.Empty;
}