using LibraryApp.Services.Interfaces;

namespace LibraryApp.Services;

public class LibraryStateProvider : ILibraryStateProvider
{
    public LibraryState LibraryState { get; private set; }
    
    public LibraryStateProvider(ISaveLoadService<LibraryState> saveLoadService)
    {
        LibraryState = saveLoadService.IsSaveExistsAsync().Result
            ? saveLoadService.LoadAsync().Result!
            : new LibraryState();
    }
    public void SetState(LibraryState libraryState)
    {
        LibraryState = libraryState;
    }
}