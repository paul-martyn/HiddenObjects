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
            IRemoteResourceLoader remoteResourceLoader = RegisterRemoteResourceLoader();
            IAssetProvider assetProvider = RegisterAssetProvider();
            IProgressService progressService = RegisterProgressService();
            IStaticDataService staticData = RegisterStaticData();
            IUIFactory uiFactory = RegisterUiFactory(staticData, assetProvider);
            IWindowService windowService = RegisterWindowService(uiFactory);
            ISaveLoadService saveLoadService = RegisterSaveLoadService(progressService);
            IGameFactory gameFactory = RegisterGameFactory(assetProvider, remoteDataService, progressService, saveLoadService, _gameStateMachine);
        }

        private void RegisterGameStateMachine(GameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IGameStateMachine>(gameStateMachine);
            _services.Single<IGameStateMachine>();
        }

        private IRemoteDataService RegisterRemoteDataService()
        {
            _services.RegisterSingle<IRemoteDataService>(new RemoteDataService());
            return _services.Single<IRemoteDataService>();
        }
        
        private IRemoteResourceLoader RegisterRemoteResourceLoader()
        {
            _services.RegisterSingle<IRemoteResourceLoader>(new RemoteResourceLoader());
            return _services.Single<IRemoteResourceLoader>();
        }

        private IProgressService RegisterProgressService()
        {
            _services.RegisterSingle<IProgressService>(new ProgressService());
            return _services.Single<IProgressService>();
        }

        private ISaveLoadService RegisterSaveLoadService(IProgressService progressService)
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService));
            return _services.Single<ISaveLoadService>();
        }

        private IAssetProvider RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            return _services.Single<IAssetProvider>();
        }

        private IGameFactory RegisterGameFactory(IAssetProvider assetProvider, IRemoteDataService remoteDataService,
            IProgressService progressService, ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine)
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, remoteDataService, progressService, saveLoadService, gameStateMachine));
            return _services.Single<IGameFactory>();
        }

        private IStaticDataService RegisterStaticData()
        {
            StaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
            return _services.Single<IStaticDataService>();
        }

        private IUIFactory RegisterUiFactory(IStaticDataService staticData, IAssetProvider assetProvider)
        {
            _services.RegisterSingle<IUIFactory>(new UIFactory(staticData, assetProvider));
            return _services.Single<IUIFactory>();
        }

        private IWindowService RegisterWindowService(IUIFactory uiFactory)
        {
            _services.RegisterSingle<IWindowService>(new WindowService(uiFactory));
            return _services.Single<IWindowService>();
        }
    }
}