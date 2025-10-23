namespace LibraryApp.Services.Interfaces;

public interface IStateProvider<T>
{
    T LibraryState { get; }
    void SetState(T libraryState);
}