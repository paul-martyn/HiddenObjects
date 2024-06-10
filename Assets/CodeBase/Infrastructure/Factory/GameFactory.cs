using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Buttons.LevelButton;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        
        private readonly IAssetProvider _assetProvider;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;

        public GameFactory(IAssetProvider assetProvider, IRemoteDataService remoteDataService,
            IProgressService progressService, ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _remoteDataService = remoteDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }

        public LevelButton CreateLevelButton(RectTransform parent)
        {
            GameObject levelButton = _assetProvider.Instantiate(path: AssetsPath.LevelButtonPath, parent);
            levelButton.GetComponent<LevelButton>().Construct(_remoteDataService, _progressService);
            RegisterProgressReaders(levelButton);
            return levelButton.GetComponent<LevelButton>();
        }

        public Level CreateLevel()
        {
            Level level = _assetProvider.Instantiate(path: AssetsPath.LevelPath).GetComponent<Level>();
            level.Construct(_remoteDataService, _gameStateMachine, _progressService, _saveLoadService);
            return level;
        }
        
        private void RegisterProgressReaders(GameObject instantiate)
        {
            foreach (ISavedProgressReader progressReader in instantiate.GetComponentsInChildren<ISavedProgressReader>())
                ProgressReaders.Add(progressReader);
        }
        
    }
}