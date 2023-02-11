using System;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public interface IGameStateMachine
    {
        public IEnterState CurrentState { get; }
        void EnterState<TState>() where TState : IEnterState;
        void RegisterFactory(Type state, IFactory<IEnterState> factory);
    }
}