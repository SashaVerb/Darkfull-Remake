using _App.Scripts.Features.Death;
using _App.Scripts.Modules.LevelManagement;
using Cysharp.Threading.Tasks;
using KinematicCharacterController.Examples;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class DeathState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerMovement _playerMovement;

        public DeathState(LevelManager levelManager, PlayerMovement playerMovement)
            : base(needsExitTime: false)
        {
            _levelManager = levelManager;
            _playerMovement = playerMovement;
        }

        public override void OnEnter()
        {
            _playerMovement.gameObject.SetActive(false);
            _levelManager.ResetLevel().Forget();
        }
    }
}
