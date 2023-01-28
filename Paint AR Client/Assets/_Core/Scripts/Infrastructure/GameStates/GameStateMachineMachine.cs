using System;
using System.Collections.Generic;
using ArPaint.Infrastructure.SceneManagement;

namespace ArPaint.Infrastructure.GameStates
{
    public class GameStateMachineMachine : IGameStateMachine
    {
        private readonly ISceneLoader _sceneLoader;
        private Dictionary<Type, IState> _states;
        
        private IState _currentState;

        [Zenject.Inject]
        public GameStateMachineMachine(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            InitStates();
        }

        private void InitStates()
        {
            _states = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(_sceneLoader),
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