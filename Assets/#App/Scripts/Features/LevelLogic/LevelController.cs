using System;
using _App.Scripts.Features.Death;
using UnityHFSM;
using VContainer.Unity;

namespace _App.Scripts.Features.LevelLogic
{
    public class LevelController : IInitializable, ITickable, IDisposable
    {
        private readonly StateMachine<LevelState> _fsm;
        private readonly PlayerHealth _playerHealth;

        public LevelController(LoadingState loadingState, GameplayState gameplayState, DeathState deathState, PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;

            _fsm = new StateMachine<LevelState>();
            _fsm.AddState(LevelState.Loading, loadingState);
            _fsm.AddState(LevelState.Gameplay, gameplayState);
            _fsm.AddState(LevelState.Death, deathState);
            
            _fsm.AddTransition(LevelState.Loading, LevelState.Gameplay);
            _fsm.AddTriggerTransitionFromAny("Death", new Transition<LevelState>(LevelState.Any, LevelState.Death, forceInstantly: true));
            
            _fsm.SetStartState(LevelState.Loading);
        }

        public void Initialize()
        {
            _playerHealth.OnDeath += HandlePlayerDeath;
            _fsm.Init();
        }

        public void Tick()
        {
            _fsm.OnLogic();
        }
        
        public void Dispose()
        {
            _playerHealth.OnDeath -= HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            _fsm.Trigger("Death");
        }
    }
}