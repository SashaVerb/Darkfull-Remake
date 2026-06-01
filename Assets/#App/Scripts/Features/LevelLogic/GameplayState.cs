using _App.Scripts.Modules.LevelManagement;
using Cysharp.Threading.Tasks;
using Features.PlayerLogic;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class GameplayState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerFacade _playerFacade;
        private readonly PlayerSpawn _playerSpawn;
        
        public GameplayState(PlayerFacade playerFacade, LevelManager levelManager, PlayerSpawn playerSpawn) : base(needsExitTime: false)
        {
            _playerFacade = playerFacade;
            _levelManager = levelManager;
            _playerSpawn = playerSpawn;
            
            _playerFacade.IsActive = false;
        }

        public override void OnEnter()
        {
            SpawnLogic().Forget();
        }

        private async UniTaskVoid SpawnLogic()
        {
            await _levelManager.FadeIn();

            _playerFacade.IsActive = true;
            _playerFacade.Teleport(_playerSpawn.GetSpawnPosition(), false);
            _playerFacade.Appear();
            _playerFacade.Unfreeze();
        }
    }
}
