using LibraryApp.Entities;
using LibraryApp.Repositories.Interfaces;
using LibraryApp.Services;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IStateProvider<LibraryState> _stateProvider;
    private readonly ISaveLoadService<LibraryState> _saveLoadService;

    public BookRepository(IStateProvider<LibraryState> stateProvider, ISaveLoadService<LibraryState> saveLoadService)
    {
        _stateProvider = stateProvider;
        _saveLoadService = saveLoadService;
    }

    public HashSet<Book> GetAll() =>
        _stateProvider.LibraryState.Books;

    public Book? GetById(string id) =>
        _stateProvider.LibraryState.Books
            .FirstOrDefault(b => b.Id == id);

    public async Task AddAsync(Book book)
    {
        _stateProvider.LibraryState.Books.Add(book);
        await _saveLoadService.SaveAsync(_stateProvider.LibraryState);
    }

    public async Task RemoveAsync(string id)
    {
        var book = GetById(id);
        if (book != null)
        {
            _stateProvider.LibraryState.Books.Remove(book);
            await _saveLoadService.SaveAsync(_stateProvider.LibraryState);
        }
    }

    public async Task UpdateAsync(Book book)
    {
        var existing = _stateProvider.LibraryState.Books
            .FirstOrDefault(b => b.Id == book.Id);

        if (existing != null)
        {
            _stateProvider.LibraryState.Books.Remove(existing);
            _stateProvider.LibraryState.Books.Add(book);
            await _saveLoadService.SaveAsync(_stateProvider.LibraryState);
        }
    }
}