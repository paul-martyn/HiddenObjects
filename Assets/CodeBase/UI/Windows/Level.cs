using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class Level : WindowBase, IRemoteImageHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private LevelProgressView _progressView;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _clickedButton;

        private IRemoteImageLoader _remoteImageLoader;
        private LoadingState _loadingState = LoadingState.Unknown;
        private int _levelID;

        private IRemoteDataService _remoteDataService;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public void Construct(IRemoteDataService remoteDataService, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _remoteDataService = remoteDataService;
        }

        private void Awake()
        {
            _remoteImageLoader = new RemoteImageLoader();
            _remoteImageLoader.LoadStateChanged += OnImageLoadStateChanged;
            _clickedButton.onClick.AddListener(OnClick);
            _closeButton.onClick.AddListener(CloseToMenu);
        }

        private void OnDestroy()
        {
            _clickedButton.onClick.RemoveListener(OnClick);
            _closeButton.onClick.RemoveListener(CloseToMenu);
            _remoteImageLoader.LoadStateChanged -= OnImageLoadStateChanged;
        }


        public async void Initialize(int lvlID)
        {
            _levelID = lvlID;
            await _remoteImageLoader.LoadImage(_remoteDataService.LevelsData.FindLevelConfigById(_levelID).LevelImageURL);
            _progressView.RefreshProgress(_progressService.Progress.FindLevelProgressById(_levelID).CurrentProgress,
                _remoteDataService.LevelsData.FindLevelConfigById(_levelID).TargetProgress);
        }

        public void OnImageLoadStateChanged(LoadingState state, Sprite resource)
        {
            if (state == _loadingState)
                return;
            _loadingState = state;
            switch (_loadingState)
            {
                case LoadingState.Loaded:
                    _image.sprite = resource;
                    break;
            }
        }

        private void OnClick()
        {
            _progressService.Progress.FindLevelProgressById(_levelID).CurrentProgress += 1;
            _saveLoadService.SaveProgress();
            _progressView.RefreshProgress(_progressService.Progress.FindLevelProgressById(_levelID).CurrentProgress,
                _remoteDataService.LevelsData.FindLevelConfigById(_levelID).TargetProgress);
            TryToComplete();
        }

        private void TryToComplete()
        {
            if (_progressService.Progress.FindLevelProgressById(_levelID).CurrentProgress ==
                _remoteDataService.LevelsData.FindLevelConfigById(_levelID).TargetProgress)
            {
                CloseToMenu();
            }
        }

        private void CloseToMenu()
        {
            AllServices.Container.Single<IWindowService>().Open(WindowID.MainMenu);
            Destroy(gameObject);
        }
    }
}