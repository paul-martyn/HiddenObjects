using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class GameInitialisationState : IState
    {
        private const string MainSceneName = "Main";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public GameInitialisationState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IUIFactory uiFactory, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public void Enter()
        {
            _sceneLoader.Load(MainSceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitializeUI();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeUI()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowID.MainMenu);
        }
        
    }
}