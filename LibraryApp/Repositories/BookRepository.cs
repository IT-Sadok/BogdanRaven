using System.Collections.Concurrent;
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

    public IReadOnlyList<Book> GetAll() => 
        _libraryState.Books.Values.ToArray();

    public Book? GetById(string id) =>
        _libraryState.Books.GetValueOrDefault(id);

    public async Task AddAsync(Book book)
    {
        _libraryState.Books.TryAdd(book.Id, book);
        await _saveLoadService.SaveAsync(_libraryState);
    }

    public async Task RemoveAsync(string id)
    {
      
        var book = GetById(id);
        if (book != null)
        {
            _libraryState.Books.TryRemove(id, out _);
            await _saveLoadService.SaveAsync(_libraryState);
        }
    }

    public async Task UpdateAsync(Book updatedBook)
    {
        if (_libraryState.Books.ContainsKey(updatedBook.Id) == false)
            return;

        _libraryState.Books[updatedBook.Id] = updatedBook;

        await _saveLoadService.SaveAsync(_libraryState);
       
    }
}