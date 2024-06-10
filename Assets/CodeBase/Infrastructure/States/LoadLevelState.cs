using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.RemoteData;
using CodeBase.Services.RemoteData.Handlers;
using CodeBase.UI.Buttons.LevelButton.States;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelPayload>, IRemoteImageHandler
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IRemoteResourceLoader _remoteResourceLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, IWindowService windowService,
            IRemoteDataService remoteDataService, IRemoteResourceLoader remoteResourceLoader,
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _remoteDataService = remoteDataService;
            _remoteResourceLoader = remoteResourceLoader;
            _gameFactory = gameFactory;
        }

        private LevelPayload _levelPayload;
        
        public async void Enter(LevelPayload levelPayload)
        {
            _windowService.Open(WindowID.Loading);
            _levelPayload = levelPayload;
            await _remoteResourceLoader.LoadImage(_remoteDataService.LevelsData.GetById(levelPayload.LevelID).LevelImageURL, OnLoadStateChanged);
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
        }

        public void OnLoadStateChanged(LoadingStatus status, Sprite remoteResource)
        {
            switch (status)
            {
                case LoadingStatus.Successfully:
                    Level level = _gameFactory.CreateLevel();
                    level.Initialize(remoteResource, _levelPayload.LevelID);
                    _windowService.MainMenuWindow.Hide();
                    _windowService.LoadingWindow.Close();
                    break;
                case LoadingStatus.Failed:
                    _levelPayload.Switcher.SwitchState<LevelButtonUnloaded>();
                    _windowService.LoadingWindow.Close();
                    break;
                case LoadingStatus.InLoading: break;
                case LoadingStatus.Unknown:
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}