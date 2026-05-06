using KinematicCharacterController.Examples;
using UnityEngine;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class GameplayState : StateBase<LevelState>
    {
        private readonly PlayerMovement _playerMovement;

        public GameplayState(PlayerMovement playerMovement) : base(needsExitTime: false)
        {
            _playerMovement = playerMovement;
        }

        public override void OnEnter()
        {
            Debug.Log("GameplayState");
            _playerMovement.enabled = true;
        }
    }
}
