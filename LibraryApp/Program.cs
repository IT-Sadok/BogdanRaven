using LibraryApp.Menu;
using LibraryApp.Repositories;
using LibraryApp.Repositories.Interfaces;
using LibraryApp.Services;
using LibraryApp.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var filePath = "books.json";
        services.AddSingleton<ISaveLoadService<LibraryState>>(sp =>
            new JsonSaveLoadService<LibraryState>(filePath));

        services.AddSingleton<ILibraryStateProvider, LibraryStateProvider>();

        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<ILibraryService, LibraryService>();

        services.AddScoped<ConsoleMenu>();
    })
    .Build();

var menu = host.Services.GetRequiredService<ConsoleMenu>();
await menu.StartAsync();