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

    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

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

    public ConcurrentDictionary<string, Book> GetAll() =>
        _libraryState.Books;

    public Book? GetById(string id) =>
        _libraryState.Books.GetValueOrDefault(id);

    public async Task AddAsync(Book book)
    {
        await _semaphore.WaitAsync();
        try
        {
            _libraryState.Books.TryAdd(book.Id, book);
            await _saveLoadService.SaveAsync(_libraryState);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task RemoveAsync(string id)
    {
        await _semaphore.WaitAsync();
        try
        {
            var book = GetById(id);
            if (book != null)
            {
                _libraryState.Books.TryRemove(id, out _);
                await _saveLoadService.SaveAsync(_libraryState);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task UpdateAsync(Book updatedBook)
    {
        await _semaphore.WaitAsync();
        try
        {
            _libraryState.Books.AddOrUpdate(
                updatedBook.Id,
                _ => updatedBook,
                (_, _) => updatedBook
            );

            await _saveLoadService.SaveAsync(_libraryState);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}