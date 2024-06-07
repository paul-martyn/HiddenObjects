namespace CodeBase.Services.Progress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(Data.Progress.PersistentProgress persistentProgress);
    }

    public interface ISavedProgressWriter : ISavedProgressReader
    { 
        void UpdateProgress(Data.Progress.PersistentProgress persistentProgress);
    }
}