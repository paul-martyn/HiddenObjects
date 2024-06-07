using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class ForcedGameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            GameBootstrapper gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            
            if (gameBootstrapper == null) 
                Instantiate(_gameBootstrapper);
        }
    }
}