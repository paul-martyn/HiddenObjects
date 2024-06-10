using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Progress;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class UnloadingLeveState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public UnloadingLeveState(GameStateMachine gameStateMachine, IWindowService windowService,
            IGameFactory gameFactory, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter()
        {
            _windowService.Open(WindowID.Loading);
            InformProgressReaders();
            _windowService.MainMenuWindow.Show();
            _gameStateMachine.Enter<GameLoopState>();
            _windowService.LoadingWindow.Close();
        }

        public void Exit()
        {
        }
        
        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
        
    }
}