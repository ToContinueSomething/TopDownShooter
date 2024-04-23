using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Inform;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.Logic.UI.Services.Factory;

namespace Sources.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services
           )
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(EntryState)] = new EntryState(this, sceneLoader, services),
                [typeof(LoadMenuState)] = new LoadMenuState(sceneLoader, services.Single<IUIFactory>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this,services.Single<IPersistentProgressService>()),
                [typeof(LoadLevelState)] = new LoadLevelState( sceneLoader,loadingCurtain, 
                    services.Single<IGameFactory>(),services.Single<IPersistentProgressService>(), 
                    services.Single<IInformProgressReaderService>())
                
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
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
