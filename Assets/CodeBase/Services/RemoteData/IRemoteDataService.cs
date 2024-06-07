using CodeBase.Data.Static.Remote;

namespace CodeBase.Services.RemoteData
{
    public interface IRemoteDataService : IService
    {
        public LevelsData LevelsData { get; }
        public void Initialize(LevelsData levelsData);
    }
}