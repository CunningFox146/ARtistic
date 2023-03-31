using System;
using System.Collections.Generic;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IFactory<IEnterState>> _stateFactories;

        public IEnterState CurrentState { get; private set; }

        public GameStateMachine(BootstrapState.Factory bootstrapFactory)
        {
            _stateFactories = new Dictionary<Type, IFactory<IEnterState>>
            {
                [typeof(BootstrapState)] = bootstrapFactory
            };
        }

        public void RegisterFactory(Type state, IFactory<IEnterState> factory)
        {
            if (_stateFactories.ContainsKey(state))
            {
                _stateFactories[state] = factory;
                return;
            }

            _stateFactories.Add(state, factory);
        }

        public void EnterState<TState>() where TState : IEnterState
        {
            UnityEngine.Debug.Log($"Enter State: {typeof(TState).Name}");
            var factory = _stateFactories[typeof(TState)];
            (CurrentState as IExitState)?.OnExit();
            CurrentState = factory.Create();
            CurrentState?.OnEnter();
        }
    }
}