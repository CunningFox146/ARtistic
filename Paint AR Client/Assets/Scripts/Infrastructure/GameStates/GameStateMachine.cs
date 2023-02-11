using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private IEnterState _currentState;
        private readonly Dictionary<Type, IFactory<IEnterState>> _stateFactories;

        public GameStateMachine(BootstrapState.Factory bootstrapFactory, ArInitState.Factory arInitStateFactory, DrawState.Factory drawStateFactory)
        {
            _stateFactories = new()
            {
                [typeof(BootstrapState)] = bootstrapFactory,
                [typeof(ArInitState)] = arInitStateFactory,
                [typeof(DrawState)] = drawStateFactory,
            };
        }

        public void EnterState<TState>() where TState : IEnterState
        {
            var factory = _stateFactories[typeof(TState)];
            (_currentState as IExitState)?.OnExit();
            _currentState = factory.Create();
            _currentState?.OnEnter();
        }
    }
}