namespace LibraryApp.Services.Interfaces;

public interface ILibraryStateProvider
{
    LibraryState LibraryState { get; }
    void SetState(LibraryState libraryState);
}