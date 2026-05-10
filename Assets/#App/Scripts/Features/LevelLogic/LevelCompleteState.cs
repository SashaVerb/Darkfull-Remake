using _App.Scripts.Modules.LevelManagement;
using Cysharp.Threading.Tasks;
using Features.PlayerLogic;
using UnityHFSM;

namespace _App.Scripts.Features.LevelLogic
{
    public class LevelCompleteState : StateBase<LevelState>
    {
        private readonly LevelManager _levelManager;
        private readonly PlayerFacade _playerFacade;

        public LevelCompleteState(LevelManager levelManager, PlayerFacade playerFacade)
            : base(needsExitTime: false)
        {
            _levelManager = levelManager;
            _playerFacade = playerFacade;
        }

        public override void OnEnter()
        {
            _playerFacade.Freeze();
            _playerFacade.Disappear();
            _levelManager.LoadNextLevel().Forget();
        }
    }
}
