using LibraryApp.Entities;
using LibraryApp.Models;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Simulation;

public class LibrarySimulation(ILibraryService libraryService)
{
    private readonly Random _random = new();

    private static readonly string[] Titles =
    {
        "Clean Code", "Dune", "The Hobbit", "Foundation", "War and Peace",
        "Harry Potter", "Inferno", "C# in Depth", "Design Patterns", "The Martian"
    };

    private static readonly string[] Authors =
    {
        "Robert Martin", "Frank Herbert", "J.R.R. Tolkien", "Isaac Asimov",
        "Leo Tolstoy", "J.K. Rowling", "Dan Brown", "Jon Skeet", "GOF", "Andy Weir"
    };

    public async Task RunAsync(int taskCount, CancellationToken token)
    {
        Console.WriteLine($"\nRunning {taskCount} concurrent simulated users...\n");

        var tasks = Enumerable.Range(1, taskCount)
            .Select(i => Task.Run(() => SimulateUserAsync(i, token), token));

        await Task.WhenAll(tasks);
    }

    private async Task SimulateUserAsync(int taskId, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                int action = _random.Next(4);

                switch (action)
                {
                    case 0:
                        await RandomAddAsync(taskId);
                        break;
                    case 1:
                        await RandomBorrowAsync(taskId);
                        break;
                    case 2:
                        await RandomReturnAsync(taskId);
                        break;
                    case 3:
                        await RandomRemoveAsync(taskId);
                        break;
                }

                await Task.Delay(_random.Next(300, 1300), token);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Task {taskId}] ERROR → {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    private async Task RandomAddAsync(int taskId)
    {
        var book = new BookModel
        {
            Id = Guid.NewGuid().ToString("N")[..6],
            Title = Titles[_random.Next(Titles.Length)],
            Author = Authors[_random.Next(Authors.Length)],
            Year = _random.Next(1950, 2024)
        };

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Task {taskId}] ADD → {book.Title}");
        Console.ResetColor();

        await libraryService.AddBookAsync(book);
    }

    private async Task RandomBorrowAsync(int taskId)
    {
        var books = libraryService.GetAllBooks()
            .Where(model => model.Status == LibraryItemStatus.Available).ToList();
        if (books.Count == 0) return;

        var book = books[_random.Next(books.Count)];

        Console.WriteLine($"[Task {taskId}] BORROW → {book.Title}");
        await libraryService.BorrowBookAsync(book.Id);
    }

    private async Task RandomReturnAsync(int taskId)
    {
        var books = libraryService.GetAllBooks().Where(model => model.Status == LibraryItemStatus.Borrowed).ToList();
        if (books.Count == 0) return;

        var book = books[_random.Next(books.Count)];

        Console.WriteLine($"[Task {taskId}] RETURN → {book.Title}");
        await libraryService.ReturnBookAsync(book.Id);
    }

    private async Task RandomRemoveAsync(int taskId)
    {
        var books = libraryService.GetAllBooks().ToList();
        if (books.Count == 0) return;

        var book = books[_random.Next(books.Count)];

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[Task {taskId}] REMOVE → {book.Title}");
        Console.ResetColor();

        await libraryService.RemoveBookAsync(book.Id);
    }
}