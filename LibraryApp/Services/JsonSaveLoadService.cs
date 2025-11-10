using System.Text.Json;
using LibraryApp.Services.Interfaces;

namespace LibraryApp.Services;

public class JsonSaveLoadService<T> : ISaveLoadService<T>
{
    private readonly string _filePath;

    public JsonSaveLoadService(string filePath)
    {
        _filePath = filePath;
    }

    public Task<bool> IsSaveExistsAsync()
        => Task.FromResult(File.Exists(_filePath));

    public async Task<T?> LoadAsync()
    {
        if (!File.Exists(_filePath))
            return default;

        await using FileStream stream = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    public async Task SaveAsync(T data)
    {
        await using FileStream stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, data,
            new JsonSerializerOptions { WriteIndented = true });
    }
}