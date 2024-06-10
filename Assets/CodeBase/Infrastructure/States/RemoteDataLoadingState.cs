using System;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Data.Static.Remote;
using CodeBase.Services.RemoteData;
using CodeBase.Services.RemoteData.Handlers;

namespace CodeBase.Infrastructure.States
{
    public class RemoteDataLoadingState : IState, IRemoteDataHandler
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ConnectionFailed _connectionFailed;
        private readonly IRemoteDataService _remoteDataService;
        private readonly IRemoteResourceLoader _remoteResourceLoader;

        public RemoteDataLoadingState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, ConnectionFailed connectionFailed,
            IRemoteDataService remoteDataService, IRemoteResourceLoader remoteResourceLoader)
        {
            _gameStateMachine = gameStateMachine;
            _remoteDataService = remoteDataService;
            _remoteResourceLoader = remoteResourceLoader;
            _loadingCurtain = loadingCurtain;
            _connectionFailed = connectionFailed;
        }

        public async void Enter()
        {
            _loadingCurtain.Show();
            await InitializeRemoteData();
        }

        public void Exit()
        {
        }

        public void OnLoadStateChanged(LoadingStatus status, string response)
        {
            switch (status)
            {
                case LoadingStatus.Failed:
                    OnConnectionFailed();
                    break;
                case LoadingStatus.Successfully:
                    OnConnectionSuccessfully(response);
                    break;
                case LoadingStatus.InLoading: break;
                case LoadingStatus.Unknown:
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }    
        }

        private async Task InitializeRemoteData() => 
            await _remoteResourceLoader.LoadData(RemoteDataPath.RemoteDataURL, OnLoadStateChanged);

        private void OnConnectionFailed() => 
            _connectionFailed.Show();

        private void OnConnectionSuccessfully(string response)
        {
            LevelsData levelsData = response.ToDeserialized<LevelsData>();
            _remoteDataService.Initialize(levelsData);
            _remoteDataService.LevelsData.Log();
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}