using System.Collections.Generic;
using _App.Scripts.Modules.LevelManagement;
using _App.Scripts.Modules.Restartable;
using Cysharp.Threading.Tasks;
using Features.PlayerLogic;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class DeathState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerFacade _playerFacade;
        private readonly PlayerSpawn _playerSpawn;
        private readonly IReadOnlyList<Restartable> _restartables;

        public DeathState(LevelManager levelManager, PlayerFacade playerFacade, PlayerSpawn playerSpawn, IReadOnlyList<Restartable> restartables)
            : base(needsExitTime: true)
        {
            _levelManager = levelManager;
            _playerFacade = playerFacade;
            _playerSpawn = playerSpawn;
            _restartables = restartables;
        }

        public override void OnEnter()
        {
            RespawnAsync().Forget();
        }

        private async UniTaskVoid RespawnAsync()
        {
            _playerFacade.Freeze();
            _playerFacade.Disappear();
            _playerSpawn.ReportDeath(_playerFacade.Position);
            
            await _levelManager.FadeOut();

            foreach (var restartable in _restartables)
            {
                restartable.ResetState();
            }

            fsm.StateCanExit();
        }
    }
}
