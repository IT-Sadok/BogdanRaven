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

    public IEnumerable<Book> GetAllBooks() =>
        _bookRepository.GetAll();

    public Book? GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Book ID cannot be empty.");

        var book = _bookRepository.GetById(id);
        if (book == null)
            throw new KeyNotFoundException($"Book with ID '{id}' not found.");

        return book;
    }

    public IEnumerable<Book> GetByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author name cannot be empty.");

        return _bookRepository.GetAll()
            .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Book> GetByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        return _bookRepository.GetAll()
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    public async Task AddBookAsync(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        if (string.IsNullOrWhiteSpace(book.Id))
            throw new ArgumentException("Book ID cannot be empty.");

        if (_bookRepository.GetById(book.Id) != null)
            throw new InvalidOperationException($"A book with ID '{book.Id}' already exists.");

        await _bookRepository.AddAsync(book);
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

        if (book.Status == ItemStatus.Borrowed)
            throw new InvalidOperationException($"Book '{book.Title}' is already borrowed.");

        book.Borrow();
        await _bookRepository.UpdateAsync(book);
    }

    public async Task ReturnBookAsync(string id)
    {
        var book = _bookRepository.GetById(id)
                   ?? throw new KeyNotFoundException($"Book with ID '{id}' not found.");

        if (book.Status == ItemStatus.Available)
            throw new InvalidOperationException($"Book '{book.Title}' is not borrowed.");

        book.Return();
        await _bookRepository.UpdateAsync(book);
    }
}