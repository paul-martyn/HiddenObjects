using CodeBase.Data.Static.Remote;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.RemoteData;
using CodeBase.UI.Buttons.LevelButton;
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
        private readonly IGameFactory _gameFactory;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IRemoteResourceLoader _resourceLoader;

        public GameInitialisationState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IUIFactory uiFactory, IWindowService windowService,
            IGameFactory gameFactory, IRemoteDataService remoteDataService, IRemoteResourceLoader resourceLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _windowService = windowService;
            _gameFactory = gameFactory;
            _remoteDataService = remoteDataService;
            _resourceLoader = resourceLoader;
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
            CreateLevelButtons();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeUI()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowID.MainMenu);
        }
        
        private async void CreateLevelButtons()
        {
            foreach (LevelConfig config in _remoteDataService.LevelsData.LevelConfigs)
            {
                LevelButton levelButton = _gameFactory.CreateLevelButton(_windowService.MainMenuWindow.ContentParent);
                levelButton.Initialize(config);
                await _resourceLoader.LoadImage(config.PreviewImageURL, levelButton.OnLoadStateChanged);
            }
        }
        
    }
}