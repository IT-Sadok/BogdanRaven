using LibraryApp.Entities;
using LibraryApp.Models;

namespace LibraryApp.Mappers;

public static class BookMapper
{
    public static Book ToEntity(this BookModel model)
    {
        var book = new Book
        {
            Id = model.Id,
            Author = model.Author,
            Status = model.Status,
            Title = model.Title,
            Year = model.Year,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ItemQualityStatus = ItemQualityStatus.New
        };
        return book;
    }

    public static BookModel ToModel(this Book entity)
    {
        var book = new BookModel
        {
            Id = entity.Id,
            Author = entity.Author,
            Status = entity.Status,
            Title = entity.Title,
            Year = entity.Year,
            ItemQualityStatus = entity.ItemQualityStatus
        };
        return book;
    }
}