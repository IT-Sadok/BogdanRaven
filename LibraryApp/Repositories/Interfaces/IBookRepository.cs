using System.Collections.Concurrent;
using LibraryApp.Entities;

namespace LibraryApp.Repositories.Interfaces;

public interface IBookRepository
{
    Task LoadState();
    IReadOnlyList<Book> GetAll();
    Book? GetById(string id);
    Task AddAsync(Book book);
    Task RemoveAsync(string id);
    Task UpdateAsync(Book book);
}