using LibraryApp.Services.Interfaces;

namespace LibraryApp.Services;

public class LibraryStateProvider : ILibraryStateProvider
{
    public LibraryState LibraryState { get; private set; }
    public void SetState(LibraryState libraryState)
    {
        LibraryState = libraryState;
    }
}