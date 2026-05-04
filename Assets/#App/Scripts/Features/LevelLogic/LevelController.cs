using System;
using UnityHFSM;
using VContainer.Unity;

namespace _App.Scripts.Features.LevelLogic
{
    public class LevelController : IInitializable, ITickable, IDisposable
    {
        private readonly StateMachine<LevelState> _fsm;

        public LevelController (LoadingState loadingState, GameplayState gameplayState)
        {
            _fsm = new StateMachine<LevelState>();
            _fsm.AddState(LevelState.Loading, loadingState);
            _fsm.AddState(LevelState.Gameplay, gameplayState);
            _fsm.AddTransition(LevelState.Loading, LevelState.Gameplay);
            _fsm.SetStartState(LevelState.Loading);
        }

        public void Initialize()
        {
            _fsm.Init();
        }

        public void Tick()
        {
            _fsm.OnLogic();
        }
        
        public void Dispose()
        {
        }
    }
}