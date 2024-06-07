using System.Collections.Generic;
using CodeBase.Data.Static.Remote;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.RemoteData;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class MainMenu :  WindowBase
    {
        [SerializeField] private UiElements.LevelButton _prefab;
        [SerializeField] private RectTransform _contentParent;
        
        private UiElements.LevelButton[] _levelButtonsInstance;
        private List<LevelConfig> LevelConfigs => _remoteDataService.LevelsData.LevelConfigs;

        private IGameFactory _gameFactory;
        private IRemoteDataService _remoteDataService;

        private void Awake()
        {
            ClearParent();
            Construct();
        }

        private void Construct()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _remoteDataService = AllServices.Container.Single<IRemoteDataService>();
        }

        protected override void Initialize()
        {
            foreach (LevelConfig config in LevelConfigs)
            {
                UiElements.LevelButton levelButton = _gameFactory.CreateLevelButton(_contentParent);
                levelButton.Initialize(config.ID, config.Name, config.PreviewImageURL, this);
            }
        }
        
        private void ClearParent()
        {
            while (_contentParent.childCount > 0) 
                DestroyImmediate(_contentParent.GetChild(0).gameObject);
        }
    }
}