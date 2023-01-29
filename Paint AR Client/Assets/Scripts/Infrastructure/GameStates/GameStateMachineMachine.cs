using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.SceneManagement;
using ArPaint.Infrastructure.GameLoop;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachineMachine : IGameStateMachine
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IUpdateLoop _updateLoop;

        private IState _currentState;
        private readonly Dictionary<Type, IState> _states;

        [Inject]
        public GameStateMachineMachine(ISceneLoader sceneLoader, IUpdateLoop updateLoop)
        {
            _sceneLoader = sceneLoader;
            _updateLoop = updateLoop;

            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader),
                [typeof(DrawState)] = new DrawState(_updateLoop)
            };

            EnterState<BootstrapState>();
        }

        public void EnterState<TState>() where TState : IState
        {
            _currentState?.OnExit();
            _currentState = _states[typeof(TState)];
            _currentState?.OnEnter();
        }
    }
}