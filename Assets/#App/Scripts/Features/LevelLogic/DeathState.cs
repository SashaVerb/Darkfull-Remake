using _App.Scripts.Features.Death;
using _App.Scripts.Modules.LevelManagement;
using Cysharp.Threading.Tasks;
using Features.PlayerLogic;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class DeathState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerFacade _playerFacade;
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerSpawn _playerSpawn;

        public DeathState(LevelManager levelManager, PlayerFacade playerFacade, PlayerHealth playerHealth, PlayerSpawn playerSpawn)
            : base(needsExitTime: true)
        {
            _levelManager = levelManager;
            _playerFacade = playerFacade;
            _playerHealth = playerHealth;
            _playerSpawn = playerSpawn;
        }

        public override void OnEnter()
        {
            _playerFacade.Freeze();
            _playerFacade.Disappear();
            RespawnAsync().Forget();
        }

        private async UniTaskVoid RespawnAsync()
        {
            _playerSpawn.ReportDeath(_playerFacade.Position);
            await _levelManager.FadeOut();
            _playerFacade.Teleport(_playerSpawn.GetSpawnPosition(), false);
            _playerHealth.Revive();
            _playerFacade.Appear();
            await _levelManager.FadeIn();
            _playerFacade.Unfreeze();
            fsm.StateCanExit();
        }
    }
}
