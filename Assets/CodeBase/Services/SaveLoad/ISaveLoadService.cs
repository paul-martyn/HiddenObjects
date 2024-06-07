namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        Data.Progress.PersistentProgress LoadProgress();
    }
}