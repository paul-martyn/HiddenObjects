using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        [SerializeField] private ConnectionFailed _connectionFailed;
       
        private GamingSession _gamingSession;

        private void Awake()
        {
            LoadingCurtain loadingCurtain = Instantiate(_loadingCurtainPrefab);
            ConnectionFailed connectionFailed = Instantiate(_connectionFailed);
            connectionFailed.Hide();
            loadingCurtain.Show();
            
            _gamingSession = new GamingSession(this, loadingCurtain, connectionFailed);
            _gamingSession.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}