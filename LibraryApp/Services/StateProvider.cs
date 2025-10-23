using LibraryApp.Services.Interfaces;

namespace LibraryApp.Services;

public class StateProvider<T> : IStateProvider<T> where T : class, new()
{
    public T LibraryState { get; private set; }

    public StateProvider(ISaveLoadService<T> saveLoadService)
    {
        if (saveLoadService.IsSaveExistsAsync().Result)
            SetState(saveLoadService.LoadAsync().Result!);
        else
            SetState(new T());
    }

    public void SetState(T libraryState) => LibraryState = libraryState;
}