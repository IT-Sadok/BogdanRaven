using LibraryApp.Entities;

namespace LibraryApp.Repositories.Interfaces;

public interface IBookRepository
{
    HashSet<Book> GetAll();
    Book? GetById(string id);
    Task AddAsync(Book book);
    Task RemoveAsync(string id);
    Task UpdateAsync(Book book);
}