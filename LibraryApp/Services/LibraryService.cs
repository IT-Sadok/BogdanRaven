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

    public IEnumerable<Book> GetAllBooks()
    {
        var books = _bookRepository.GetAll();
        return books.OrderBy(b => b.Title);
    }

    public Book? GetById(string id) => 
        _bookRepository.GetById(id);

    public IEnumerable<Book> GetByAuthor(string author)
    {
        var books = _bookRepository.GetAll();
        return books
            .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
            .OrderBy(b => b.Title);
    }

    public IEnumerable<Book> GetByTitle(string title)
    {
        var books = _bookRepository.GetAll();
        return books
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .OrderBy(b => b.Author);
    }

    public async Task AddBookAsync(Book book) =>
        await _bookRepository.AddAsync(book);

    public async Task RemoveBookAsync(string id) =>
        await _bookRepository.RemoveAsync(id);

    public async Task BorrowBookAsync(string id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            throw new InvalidOperationException("Book not found.");

        book.Borrow();
        await _bookRepository.UpdateAsync(book);
    }

    public async Task ReturnBookAsync(string id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            throw new InvalidOperationException("Book not found.");

        book.Return();
        await _bookRepository.UpdateAsync(book);
    }
}