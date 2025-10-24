using LibraryApp.Entities;
using LibraryApp.Models;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Menu;

public class ConsoleMenu
{
    private readonly ILibraryService _libraryService;

    public ConsoleMenu(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    private async Task ExecuteSafe(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"⚠️  {ex.Message}");
            Console.ResetColor();
        }
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== LIBRARY MENU =====");
            Console.WriteLine("1. Show all books");
            Console.WriteLine("2. Add book");
            Console.WriteLine("3. Remove book");
            Console.WriteLine("4. Borrow book");
            Console.WriteLine("5. Return book");
            Console.WriteLine("6. Search by author");
            Console.WriteLine("7. Search by title");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowAllBooks();
                    break;
                case "2":
                    await AddBookAsync();
                    break;
                case "3":
                    await RemoveBookAsync();
                    break;
                case "4":
                    await BorrowBookAsync();
                    break;
                case "5":
                    await ReturnBookAsync();
                    break;
                case "6":
                    SearchByAuthor();
                    break;
                case "7":
                    SearchByTitle();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void ShowAllBooks()
    {
        ExecuteSafe(async () =>
        {
            Console.WriteLine("\n=== Books in Library ===");
            foreach (var book in _libraryService.GetAllBooks())
                Console.WriteLine(book);
        });
    }

    private async Task AddBookAsync()
    {
        await ExecuteSafe(async () =>
        {
            Console.Write("Enter ID: ");
            var id = Console.ReadLine() ?? "";

            Console.Write("Enter Title: ");
            var title = Console.ReadLine() ?? "";

            Console.Write("Enter Author: ");
            var author = Console.ReadLine() ?? "";

            Console.Write("Enter Year: ");
            var year = int.TryParse(Console.ReadLine(), out var y) ? y : 0;

            var book = new BookModel()
            {
                Id = id,
                Title = title,
                Author = author,
                Year = year
            };
            await _libraryService.AddBookAsync(book);
            Console.WriteLine("✅ Book added successfully!");
        });
    }

    private async Task RemoveBookAsync()
    {
        await ExecuteSafe(async () =>
        {
            Console.Write("Enter Book ID to remove: ");
            var id = Console.ReadLine() ?? string.Empty;
            await _libraryService.RemoveBookAsync(id);
            Console.WriteLine("✅ Book removed!");
        });
    }

    private async Task BorrowBookAsync()
    {
        await ExecuteSafe(async () =>
        {
            Console.Write("Enter Book ID to borrow: ");
            var id = Console.ReadLine() ?? string.Empty;
            await _libraryService.BorrowBookAsync(id);
            Console.WriteLine("📕 Book borrowed!");
        });
    }

    private async Task ReturnBookAsync()
    {
        await ExecuteSafe(async () =>
        {
            Console.Write("Enter Book ID to return: ");
            var id = Console.ReadLine() ?? string.Empty;
            await _libraryService.ReturnBookAsync(id);
            Console.WriteLine("📗 Book returned!");
        });
    }

    private void SearchByAuthor()
    {
        ExecuteSafe(async () =>
        {
            Console.Write("Enter Author name: ");
            var author = Console.ReadLine() ?? string.Empty;
            var books = _libraryService.GetByAuthor(author);
            foreach (var book in books)
                Console.WriteLine(book);
        });
    }

    private void SearchByTitle()
    {
        ExecuteSafe(async () =>
        {
            Console.Write("Enter Title: ");
            var title = Console.ReadLine() ?? string.Empty;
            var books = _libraryService.GetByTitle(title);
            foreach (var book in books)
                Console.WriteLine(book);
        });
    }
}