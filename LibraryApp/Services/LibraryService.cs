using LibraryApp.Entities;
using LibraryApp.Mappers;
using LibraryApp.Models;
using LibraryApp.Repositories.Interfaces;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Services;

public class LibraryService : ILibraryService
{
    private readonly IBookRepository _bookRepository;

    public LibraryService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<BookModel> GetAllBooks() =>
        _bookRepository.GetAll().Select(b => b.ToModel());

    public BookModel? GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Book ID cannot be empty.");

        var book = _bookRepository.GetById(id);
        if (book == null)
            throw new KeyNotFoundException($"Book with ID '{id}' not found.");

        return book.ToModel();
    }

    public IEnumerable<BookModel> GetByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author name cannot be empty.");

        return _bookRepository.GetAll()
            .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).Select(book => book.ToModel());
    }

    public IEnumerable<BookModel> GetByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        return _bookRepository.GetAll()
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).Select(book => book.ToModel());
    }

    public async Task AddBookAsync(BookModel book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        if (string.IsNullOrWhiteSpace(book.Id))
            throw new ArgumentException("Book ID cannot be empty.");

        if (_bookRepository.GetById(book.Id) != null)
            throw new InvalidOperationException($"A book with ID '{book.Id}' already exists.");

        await _bookRepository.AddAsync(book.ToEntity());
    }

    public async Task RemoveBookAsync(string id)
    {
        var existing = _bookRepository.GetById(id);
        if (existing == null)
            throw new KeyNotFoundException($"Cannot remove — book with ID '{id}' does not exist.");

        await _bookRepository.RemoveAsync(id);
    }

    public async Task BorrowBookAsync(string id)
    {
        var book = _bookRepository.GetById(id)
                   ?? throw new KeyNotFoundException($"Book with ID '{id}' not found.");

        if (book.Status == LibraryItemStatus.Borrowed)
            throw new InvalidOperationException($"Book '{book.Title}' is already borrowed.");

        var updatedBook = book with { Status = LibraryItemStatus.Borrowed, UpdatedAt = DateTime.UtcNow };
        await _bookRepository.UpdateAsync(updatedBook);
    }

    public async Task ReturnBookAsync(string id)
    {
        var book = _bookRepository.GetById(id)
                   ?? throw new KeyNotFoundException($"Book with ID '{id}' not found.");

        if (book.Status == LibraryItemStatus.Available)
            throw new InvalidOperationException($"Book '{book.Title}' is not borrowed.");

        var updatedBook = book with
        {
            Status = LibraryItemStatus.Borrowed,
            BorrowCount = book.BorrowCount + 1,
            UpdatedAt = DateTime.UtcNow,
            ItemQualityStatus = GetQualityStatus(book.BorrowCount + 1)
        };
        await _bookRepository.UpdateAsync(updatedBook);
    }

    private ItemQualityStatus GetQualityStatus(int borrowCount)
    {
        return borrowCount switch
        {
            < 1 => ItemQualityStatus.New,
            < 6 => ItemQualityStatus.Good,
            < 16 => ItemQualityStatus.Used,
            < 31 => ItemQualityStatus.Damaged,
            _ => ItemQualityStatus.Lost
        };
    }
}