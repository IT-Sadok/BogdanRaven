using LibraryApp.Entities;
using LibraryApp.Models;

namespace LibraryApp.Services.Interfaces;

public interface ILibraryService
{
    IEnumerable<BookModel> GetAllBooks();
    BookModel? GetById(string id);
    IEnumerable<BookModel> GetByAuthor(string author);
    IEnumerable<BookModel> GetByTitle(string title);
    Task AddBookAsync(BookModel book);
    Task RemoveBookAsync(string id);
    Task BorrowBookAsync(string id);
    Task ReturnBookAsync(string id);
}