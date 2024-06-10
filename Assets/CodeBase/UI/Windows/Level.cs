using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.LevelProgress;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private LevelProgressView _progressView;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _clickedButton;

        private int _levelID;
        private int CurrentProgress => _progressService.Progress.GetById(_levelID).CurrentProgress;
        private int TargetProgress => _remoteDataService.LevelsData.GetById(_levelID).TargetProgress;
        private bool IsComplete => CurrentProgress == TargetProgress;
        
        private IRemoteDataService _remoteDataService;
        private IGameStateMachine _stateMachine;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public void Construct(IRemoteDataService remoteDataService, IGameStateMachine stateMachine,
            IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _stateMachine = stateMachine;
            _progressService = progressService;
            _remoteDataService = remoteDataService;
        }

        private void Awake()
        {
            _clickedButton.onClick.AddListener(OnClick);
            _closeButton.onClick.AddListener(CloseToMenu);
        }

        private void OnDestroy()
        {
            _clickedButton.onClick.RemoveListener(OnClick);
            _closeButton.onClick.RemoveListener(CloseToMenu);
        }
        
        public void Initialize(Sprite sprite, int levelID)
        {
            _levelID = levelID;
            _image.sprite = sprite;
            RefreshProgressView();
        }

        private void OnClick()
        {
            IncreaseProgress();
            RefreshProgressView();
            TryToComplete();
        }

        private void RefreshProgressView() => 
            _progressView.RefreshProgress(CurrentProgress, TargetProgress);

        private void IncreaseProgress()
        {
            _progressService.Progress.GetById(_levelID).CurrentProgress += 1;
            _saveLoadService.SaveProgress();
        }

        private void TryToComplete()
        {
            if (IsComplete) 
                CloseToMenu();
        }

        private void CloseToMenu()
        {
            _stateMachine.Enter<UnloadingLeveState>();
            Dispose();
        }

        private void Dispose() => 
            Destroy(gameObject);
    }
}