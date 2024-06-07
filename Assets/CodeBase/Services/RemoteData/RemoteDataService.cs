using CodeBase.Data.Static.Remote;

namespace CodeBase.Services.RemoteData
{
    internal class RemoteDataService : IRemoteDataService
    {
        public LevelsData LevelsData { get; private set; }
        
        public void Initialize(LevelsData levelsData) => 
            LevelsData = levelsData;
    }
}