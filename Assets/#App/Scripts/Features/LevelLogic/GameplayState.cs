using Features.PlayerLogic;
using UnityEngine;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class GameplayState : StateBase<LevelState>
    {
        private readonly PlayerFacade _playerFacade;

        public GameplayState(PlayerFacade playerFacade) : base(needsExitTime: false)
        {
            _playerFacade = playerFacade;
        }

        public override void OnEnter()
        {
            Debug.Log("GameplayState");
            _playerFacade.Unfreeze();
        }
    }
}
