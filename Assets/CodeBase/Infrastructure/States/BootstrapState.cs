using CodeBase.EventBus;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private const string BootstrapSceneName = "Bootstrap";

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(name: BootstrapSceneName, onLoaded: EnterRemoteDataLoadingState);
        }

        public void Exit()
        {
        }

        private void EnterRemoteDataLoadingState() =>
            _gameStateMachine.Enter<RemoteDataLoadingState>();

        private void RegisterServices()
        {
            RegisterGameStateMachine(_gameStateMachine);
            IRemoteDataService remoteDataService = RegisterRemoteDataService();
            IAssetProvider assetProvider = RegisterAssetProvider();
            IProgressService progressService = RegisterProgressService();
            IStaticDataService staticData = RegisterStaticData();
            IUIFactory uiFactory = RegisterUiFactory(staticData, assetProvider, _gameStateMachine);
            IWindowService windowService = RegisterWindowService(uiFactory);
            IGameFactory gameFactory = RegisterGameFactory(assetProvider, uiFactory, remoteDataService, progressService);
            ISaveLoadService saveLoadService = RegisterSaveLoadService(progressService, gameFactory);
            IEventBus eventBus = RegisterEventBus();
        }

        private void RegisterGameStateMachine(GameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IGameStateMachine>(gameStateMachine);
            _services.Single<IGameStateMachine>();
        }

        private IProgressService RegisterProgressService()
        {
            _services.RegisterSingle<IProgressService>(new ProgressService());
            return _services.Single<IProgressService>();
        }

        private ISaveLoadService RegisterSaveLoadService(IProgressService progressService, IGameFactory gameFactory)
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService, gameFactory));
            return _services.Single<ISaveLoadService>();
        }

        private IRemoteDataService RegisterRemoteDataService()
        {
            _services.RegisterSingle<IRemoteDataService>(new RemoteDataService());
            return _services.Single<IRemoteDataService>();
        }

        private IAssetProvider RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            return _services.Single<IAssetProvider>();
        }

        private IGameFactory RegisterGameFactory(IAssetProvider assetProvider, IUIFactory uiFactory,
            IRemoteDataService remoteDataService, IProgressService progressService)
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, uiFactory, remoteDataService, progressService));
            return _services.Single<IGameFactory>();
        }

        private IEventBus RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(new EventBus.EventBus());
            return _services.Single<IEventBus>();
        }

        private IStaticDataService RegisterStaticData()
        {
            StaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
            return _services.Single<IStaticDataService>();
        }

        private IUIFactory RegisterUiFactory(IStaticDataService staticData, IAssetProvider assetProvider, IGameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IUIFactory>(new UIFactory(staticData, assetProvider, gameStateMachine));
            return _services.Single<IUIFactory>();
        }

        private IWindowService RegisterWindowService(IUIFactory uiFactory)
        {
            _services.RegisterSingle<IWindowService>(new WindowService(uiFactory));
            return _services.Single<IWindowService>();
        }
    }
}