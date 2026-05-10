using _App.Scripts.Modules.LevelManagement;
using Features.PlayerLogic;
using UnityEngine;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class LoadingState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerFacade _playerFacade;

        public LoadingState(LevelManager levelManager, PlayerFacade playerFacade) : base(needsExitTime: true)
        {
            _levelManager = levelManager;
            _playerFacade = playerFacade;
        }

        public override void OnEnter()
        {
            Debug.Log("LoadingState");
            _playerFacade.Freeze();
        }

        public override void OnLogic()
        {
            if (!_levelManager.IsLoading)
                fsm.StateCanExit();
        }
    }
}
