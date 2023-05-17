using Services.Auth;
using Zenject;

namespace ArPaint.Infrastructure.GameStates
{
    public class AuthState : IEnterState, IExitState
    {
        private readonly IAuthSystem _auth;

        public AuthState(IAuthSystem auth)
        {
            _auth = auth;
        }
        
        public void OnEnter()
        {
            _auth.AuthStateChange -= OnAuthStateChange;
        }

        public void OnExit()
        {
            _auth.AuthStateChange -= OnAuthStateChange;
        }

        private void OnAuthStateChange(bool isSignedIn)
        {
            if (isSignedIn)
            {
                _auth.AuthStateChange -= OnAuthStateChange;
                UnityEngine.Debug.Log("Signed in!");
            }
        }

        public class Factory : PlaceholderFactory<AuthState>{}
    }
}