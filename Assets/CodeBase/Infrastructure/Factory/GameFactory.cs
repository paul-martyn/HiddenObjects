using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.UiElements;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new();

        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IProgressService _progressService;

        public GameFactory(IAssetProvider assetProvider, IUIFactory uiFactory,
            IRemoteDataService remoteDataService, IProgressService progressService)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _remoteDataService = remoteDataService;
            _progressService = progressService;
        }

        public LevelButton CreateLevelButton(RectTransform parent)
        {
            GameObject levelButton = _assetProvider.Instantiate(path: AssetsPath.LevelButtonPath, parent);
            levelButton.GetComponent<LevelButton>().Construct(_remoteDataService, _progressService);
            return levelButton.GetComponent<LevelButton>();
        }

        public Level CreateLevel(int levelID)
        {
            Level level = _uiFactory.CreatLevelWindow();
            level.Construct(_remoteDataService, _progressService, AllServices.Container.Single<ISaveLoadService>());
            level.Initialize(levelID);
            return level;
        }
    }
}