using LibraryApp.Models;

namespace LibraryApp.Services.Interfaces;

public interface ILibraryService
{
    IEnumerable<Book> GetAllBooks();
    Book? GetById(string id);
    IEnumerable<Book> GetByAuthor(string author);
    IEnumerable<Book> GetByTitle(string title);
    Task AddBookAsync(Book book);
    Task RemoveBookAsync(string id);
    Task BorrowBookAsync(string id);
    Task ReturnBookAsync(string id);
}