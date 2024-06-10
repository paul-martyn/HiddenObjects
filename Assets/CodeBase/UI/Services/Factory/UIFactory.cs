using CodeBase.Data.Static;
using CodeBase.Infrastructure.AssetManagement;
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

        public UIFactory(IStaticDataService staticData, IAssetProvider assetProvider)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
        }

        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate(AssetsPath.RootCanvasPath);
            _uiRoot = root.transform;
        }

        public MainMenuWindow CreateMainMenuWindow() => 
            InstantiateWindow(WindowID.MainMenu).GetComponent<MainMenuWindow>();

        public LoadingWindow CreateLoadingWindow() => 
            InstantiateWindow(WindowID.Loading).GetComponent<LoadingWindow>();

        private WindowBase InstantiateWindow(WindowID windowID)
        {
            WindowConfig config = _staticData.ForWindow(windowID);
            WindowBase windowBase = Object.Instantiate(config.Prefab, _uiRoot);
            return windowBase;
        }
        
    }
}