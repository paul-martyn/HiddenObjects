using CodeBase.EventBus;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        protected IEventBus _eventBus;
        protected IGameStateMachine _gameStateMachine;
        
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Close() => 
            Destroy(gameObject);

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}