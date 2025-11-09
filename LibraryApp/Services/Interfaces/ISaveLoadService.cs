namespace LibraryApp.Services.Interfaces;

public interface ISaveLoadService<T>
{
    Task<T?> LoadAsync();
    Task SaveAsync(T data);
    Task<bool> IsSaveExistsAsync();
}