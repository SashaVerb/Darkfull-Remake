using _App.Scripts.Modules.LevelManagement;
using KinematicCharacterController.Examples;
using UnityEngine;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class LoadingState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerMovement _playerMovement;

        public LoadingState(LevelManager levelManager, PlayerMovement playerMovement) : base(needsExitTime: true)
        {
            _levelManager = levelManager;
            _playerMovement = playerMovement;
        }

        public override void OnEnter()
        {
            if (!_levelManager.IsLoading)
            {
                HandleLevelReady();
                return;
            }
            
            _playerMovement.enabled = false;
            _levelManager.OnLevelReady += HandleLevelReady;
        }

        public override void OnExit()
        {
            _levelManager.OnLevelReady -= HandleLevelReady;
        }

        private void HandleLevelReady()
        {
            fsm.StateCanExit();
        }
    }
}
