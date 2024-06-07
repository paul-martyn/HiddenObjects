using System;
using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.Services.Progress;
using CodeBase.Services.RemoteData;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private IState _activeState;
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, LoadingCurtain loadingCurtain, ConnectionFailed connectionFailed)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(RemoteDataLoadingState)] = new RemoteDataLoadingState(this, services.Single<IRemoteDataService>(), loadingCurtain, connectionFailed),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IRemoteDataService>(),
                    services.Single<IProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(GameInitialisationState)] = new GameInitialisationState(this, sceneLoader, loadingCurtain, services.Single<IUIFactory>(), services.Single<IWindowService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState => 
            _states[typeof(TState)] as TState;
    }
}