using CodeBase.Data.Static;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameStateMachine _gameStateMachine;

        public UIFactory(IStaticDataService staticData, IAssetProvider assetProvider, IGameStateMachine gameStateMachine)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate(AssetsPath.RootCanvasPath);
            _uiRoot = root.transform;
        }

        public void CreatTroubleWindow() => 
            InstantiateWindow(WindowID.Trouble);

        public void CreateMainMenuWindow() => 
            InstantiateWindow(WindowID.MainMenu);

        public Level CreatLevelWindow() => 
             InstantiateWindow(WindowID.Level).GetComponent<Level>();

        public void CreateCompleteWindow() => 
            InstantiateWindow(WindowID.Complete);

        private WindowBase InstantiateWindow(WindowID windowID)
        {
            WindowConfig config = _staticData.ForWindow(windowID);
            WindowBase windowBase = Object.Instantiate(config.Prefab, _uiRoot);
            windowBase.Construct(_gameStateMachine);
            return windowBase;
        }
        
    }
}