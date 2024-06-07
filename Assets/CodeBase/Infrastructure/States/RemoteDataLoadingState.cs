using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Data.Static.Remote;
using CodeBase.Services.RemoteData;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.States
{
    public class RemoteDataLoadingState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IRemoteDataService _remoteDataService;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ConnectionFailed _connectionFailed;

        public RemoteDataLoadingState(GameStateMachine gameStateMachine, IRemoteDataService remoteDataService,
            LoadingCurtain loadingCurtain, ConnectionFailed connectionFailed)
        {
            _gameStateMachine = gameStateMachine;
            _remoteDataService = remoteDataService;
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

        private async Task InitializeRemoteData()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(RemoteDataPath.RemoteDataURL);

            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                _connectionFailed.Show();
            }
            else
            { 
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log("Received: " + jsonResponse);
                LevelsData levelsData = jsonResponse.ToDeserialized<LevelsData>();
                _remoteDataService.Initialize(levelsData);
                _remoteDataService.LevelsData.Log();
                _gameStateMachine.Enter<LoadProgressState>();
            }
        }
        
    }
}