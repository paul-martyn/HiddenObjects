using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.UI.LevelButton;
using CodeBase.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.UiElements
{
    public class LevelButton : MonoBehaviour, IRemoteImageHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LoadingProcessAnimation _loadingProcessAnimation;
        [SerializeField] private LevelProgressView _levelProgressView;
        [Space(10f)]
        [SerializeField] private ButtonCustomAnimation _buttonCustomAnimation;

        public int LevelID => _levelID;
        private int _levelID;
        
        public MainMenu MainMenu => _mainMenu;
        private MainMenu _mainMenu;

        private IRemoteImageLoader _remoteImageLoader;
        private LoadingState _loadingState = LoadingState.Unknown;
        private LevelButtonState _currentButtonState;
        private List<LevelButtonState> _allStates;
        
        private IRemoteDataService _remoteDataService;
        private IProgressService _progressService;
        private int TargetProgress => _remoteDataService.LevelsData.FindLevelConfigById(_levelID).TargetProgress;
        private int CurrentProgress => _progressService.Progress.FindLevelProgressById(_levelID).CurrentProgress;

        public void Construct(IRemoteDataService remoteDataService, IProgressService progressService)
        {
            _remoteDataService = remoteDataService;
            _progressService = progressService;
        }

        private void Awake()
        {
            _allStates = new List<LevelButtonState>
            {
                new LevelButtonLoadingState(_loadingProcessAnimation),
                new LevelButtonActive(_canvasGroup, _button, _image, _buttonCustomAnimation, this),
                new LevelButtonInactive(_canvasGroup, _button, _image),
                new LevelButtonComplete(_canvasGroup, _button, _image)
            };
            _remoteImageLoader = new RemoteImageLoader();
            _remoteImageLoader.LoadStateChanged += OnImageLoadStateChanged;        
            _button.onClick.AddListener(OnClick);        
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);        
            _remoteImageLoader.LoadStateChanged -= OnImageLoadStateChanged;
        }

        public async void Initialize(int levelID, string title, string previewURL, MainMenu mainMenu)
        {
            _levelID = levelID;
            _title.SetText(title);
            _mainMenu = mainMenu;
            await _remoteImageLoader.LoadImage(previewURL);
        }

        public void OnImageLoadStateChanged(LoadingState state, Sprite resource)
        {
            if (state == _loadingState)
                return;
            _loadingState = state;
            switch (_loadingState)
            {
                case LoadingState.InLoading:
                    SwitchToState<LevelButtonLoadingState>();
                    break;
                case LoadingState.Loaded:
                    SetSprite(resource);
                    CheckOnCompleting();
                    break;
                case LoadingState.Failed:
                    SwitchToState<LevelButtonInactive>();
                    break;
            }        
        }

        private void CheckOnCompleting()
        {
            _levelProgressView.RefreshProgress(CurrentProgress, TargetProgress);
            if (CurrentProgress < TargetProgress)
                SwitchToState<LevelButtonActive>();
            else
                SwitchToState<LevelButtonComplete>();
        }

        private void SwitchToState<T>() where T : LevelButtonState
        {
            LevelButtonState state = _allStates.FirstOrDefault(levelButtonState => levelButtonState is T);
            if (state == null) return;
            _currentButtonState?.Exit();
            _currentButtonState = state;
            _currentButtonState.Enter();
        }

        private void OnClick() => 
            _currentButtonState.OnClick();

        private void SetSprite(Sprite sprite) => 
            _image.sprite = sprite;
        
    }
}
