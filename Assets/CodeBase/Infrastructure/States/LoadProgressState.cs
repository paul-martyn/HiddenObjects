using CodeBase.Data.Progress;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine, IRemoteDataService remoteDataService,
            IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _remoteDataService = remoteDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<GameInitialisationState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            _saveLoadService.SaveProgress();
        }

        private PersistentProgress NewProgress()
        {
            PersistentProgress persistentProgress = new(_remoteDataService.LevelsData.LevelConfigs);
            return persistentProgress;
        }
    }
}