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
        private readonly LevelComplete _levelComplete;

        public LevelController(LoadingState loadingState, GameplayState gameplayState, DeathState deathState, LevelCompleteState levelCompleteState, PlayerHealth playerHealth, LevelComplete levelComplete)
        {
            _playerHealth = playerHealth;
            _levelComplete = levelComplete;

            _fsm = new StateMachine<LevelState>();
            _fsm.AddState(LevelState.Loading, loadingState);
            _fsm.AddState(LevelState.Gameplay, gameplayState);
            _fsm.AddState(LevelState.Death, deathState);
            _fsm.AddState(LevelState.LevelComplete, levelCompleteState);
            
            _fsm.AddTransition(LevelState.Loading, LevelState.Gameplay);
            _fsm.AddTransition(LevelState.Death, LevelState.Gameplay);
            _fsm.AddTriggerTransitionFromAny("Death", new Transition<LevelState>(LevelState.Any, LevelState.Death, forceInstantly: true));
            _fsm.AddTriggerTransitionFromAny("LevelComplete", new Transition<LevelState>(LevelState.Any, LevelState.LevelComplete, forceInstantly: true));
            
            _fsm.SetStartState(LevelState.Loading);
        }

        public void Initialize()
        {
            _playerHealth.OnDeath.AddListener(HandlePlayerDeath);
            _levelComplete.OnLevelComplete += HandleLevelComplete;
            
            _fsm.Init();
        }

        public void Tick()
        {
            _fsm.OnLogic();
        }
        
        public void Dispose()
        {
            _playerHealth.OnDeath.RemoveListener(HandlePlayerDeath);
            _levelComplete.OnLevelComplete -= HandleLevelComplete;
        }

        private void HandlePlayerDeath()
        {
            _fsm.Trigger("Death");
        }

        private void HandleLevelComplete()
        {
            _fsm.Trigger("LevelComplete");
        }
    }
}