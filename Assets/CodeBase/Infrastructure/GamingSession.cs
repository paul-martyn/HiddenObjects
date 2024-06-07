using CodeBase.Infrastructure.States;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
    public class GamingSession
    {
        public readonly GameStateMachine StateMachine;

        public GamingSession(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, ConnectionFailed connectionFailed)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, loadingCurtain, connectionFailed);
        }
    }
}