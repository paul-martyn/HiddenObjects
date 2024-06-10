using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
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
        private IExitableState _activeState;
        private readonly Dictionary<Type, IExitableState> _states;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, LoadingCurtain loadingCurtain, ConnectionFailed connectionFailed)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                
                [typeof(RemoteDataLoadingState)] = new RemoteDataLoadingState(this, loadingCurtain, connectionFailed,
                    services.Single<IRemoteDataService>(), services.Single<IRemoteResourceLoader>()),
                
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IRemoteDataService>(),
                    services.Single<IProgressService>(), services.Single<ISaveLoadService>()),
                
                [typeof(GameInitialisationState)] = new GameInitialisationState(this, sceneLoader, loadingCurtain, services.Single<IUIFactory>(), 
                    services.Single<IWindowService>(), services.Single<IGameFactory>(), services.Single<IRemoteDataService>(),
                    services.Single<IRemoteResourceLoader>()),
                
                [typeof(GameLoopState)] = new GameLoopState(this),
                
                [typeof(LoadLevelState)] = new LoadLevelState(this, services.Single<IWindowService>(),
                    services.Single<IRemoteDataService>(), services.Single<IRemoteResourceLoader>(),
                    services.Single<IGameFactory>()),
                
                [typeof(UnloadingLeveState)] = new UnloadingLeveState(this, services.Single<IWindowService>(), 
                    services.Single<IGameFactory>(), services.Single<IProgressService>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}