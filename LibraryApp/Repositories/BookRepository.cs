using LibraryApp.Entities;
using LibraryApp.Repositories.Interfaces;
using LibraryApp.Services;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ISaveLoadService<LibraryState> _saveLoadService;
    private LibraryState _libraryState;

    public BookRepository(ISaveLoadService<LibraryState> saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }

    public async Task LoadState()
    {
        bool hasSave = await _saveLoadService.IsSaveExistsAsync();
        if (hasSave)
            _libraryState = await _saveLoadService.LoadAsync();
        else
            _libraryState = new LibraryState();
    }

    public HashSet<Book> GetAll() =>
        _libraryState.Books;

    public Book? GetById(string id) =>
        _libraryState.Books
            .FirstOrDefault(b => b.Id == id);

    public async Task AddAsync(Book book)
    {
        var newBook = new Book()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
        };
        _libraryState.Books.Add(book);
        _libraryState.Books.Add(newBook);
        await _saveLoadService.SaveAsync(_libraryState);
    }

    public async Task RemoveAsync(string id)
    {
        var book = GetById(id);
        if (book != null)
        {
            _libraryState.Books.Remove(book);
            await _saveLoadService.SaveAsync(_libraryState);
        }
    }

    public async Task UpdateAsync(Book book)
    {
        var existing = _libraryState.Books
            .FirstOrDefault(b => b.Id == book.Id);

        if (existing != null)
        {
            _libraryState.Books.Remove(existing);
            _libraryState.Books.Add(book);
            await _saveLoadService.SaveAsync(_libraryState);
        }
    }
}