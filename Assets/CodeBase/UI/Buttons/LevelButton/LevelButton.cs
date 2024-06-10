using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Progress;
using CodeBase.Data.Static.Remote;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.RemoteData.Handlers;
using CodeBase.UI.Buttons.LevelButton.States;
using CodeBase.UI.LevelProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons.LevelButton
{
    public class LevelButton : ButtonAddon, ILevelButtonStateSwitcher, IRemoteImageHandler, ISavedProgressReader
    {
        [Space(10f)]
        [SerializeField] private Image _preview;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LoadingProcessAnimation _loadingProcessAnimation;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private GameObject _failPattern;

        private int _levelID;
        private int CurrentProgress => _progressService.Progress.GetById(_levelID).CurrentProgress;
        private int TargetProgress => _remoteDataService.LevelsData.GetById(_levelID).TargetProgress;

        private List<LevelButtonState> _allStates;
        private LevelButtonState _currentState;
        
        private IRemoteDataService _remoteDataService;
        private IProgressService _progressService;

        public void Construct(IRemoteDataService remoteDataService, IProgressService progressService)
        {
            _remoteDataService = remoteDataService;
            _progressService = progressService;
        }
        
        public void Initialize(LevelConfig levelConfig)
        {
            _levelID = levelConfig.ID;
            _title.SetText(levelConfig.Name);
            _levelProgressView.RefreshProgress(CurrentProgress, levelConfig.TargetProgress);
            _allStates = new List<LevelButtonState>
            {
                new LevelButtonInLoading(buttonAddon: this, _loadingProcessAnimation),
                new LevelButtonActive(buttonAddon: this, _levelID, switcher: this),
                new LevelButtonUnloaded(buttonAddon: this, _preview, _failPattern),
                new LevelButtonComplete(buttonAddon: this, _preview)
            };
            SwitchState<LevelButtonInLoading>();
        }

        protected override void OnClick()
        {
            base.OnClick();
            _currentState.OnClick();
        }

        public void OnLoadStateChanged(LoadingStatus status, Sprite remoteResource)
        {
            switch (status)
            {
                case LoadingStatus.InLoading:
                    SwitchState<LevelButtonInLoading>();
                    break;
                case LoadingStatus.Successfully:
                    _preview.sprite = remoteResource;
                    LevelCheck();
                    break;
                case LoadingStatus.Failed:
                    SwitchState<LevelButtonUnloaded>();
                    break;
                case LoadingStatus.Unknown:
                default: 
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
        
        public void SwitchState<T>() where T : LevelButtonState
        {
            LevelButtonState state = _allStates.FirstOrDefault(levelButtonState => levelButtonState is T);
            if (state == null || state == _currentState) 
                return;
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();        
        }
        
        private void LevelCheck()
        {
            if (CurrentProgress < TargetProgress)
                SwitchState<LevelButtonActive>();
            else
                SwitchState<LevelButtonComplete>();
        }

        public void LoadProgress(PersistentProgress persistentProgress) => 
            _levelProgressView.RefreshProgress(CurrentProgress, TargetProgress);
    }
}