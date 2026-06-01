using System.Threading;
using _App.Scripts.Features.Death;
using Cysharp.Threading.Tasks;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine;

namespace Features.PlayerLogic
{
    public class PlayerFacade
    {
        private readonly PlayerMovement _movement;
        private readonly BeamShooter _beamShooter;
        private readonly PlayerHealth _playerHealth;
        private readonly GameObject _player;
        private readonly KinematicCharacterMotor _motor;
        private readonly EnableStateEvents _enableStateEvents;
        private CancellationTokenSource _beamCts;
        
        public bool IsActive
        {
            get => _player.activeSelf;
            set => _player.SetActive(value);
        }
        
        public Vector3 Position => _player.transform.position;
        
        public PlayerFacade(PlayerMovement movement, BeamShooter beamShooter, PlayerHealth playerHealth, KinematicCharacterMotor motor, GameObject player, EnableStateEvents enableStateEvents)
        {
            _movement = movement;
            _beamShooter = beamShooter;
            _playerHealth = playerHealth;
            _motor = motor;
            _player = player;
            _enableStateEvents = enableStateEvents;
        }

        public void Freeze()
        {
            CancelBeamTimer();
            _movement.enabled = false;
            _beamShooter.enabled = false;
        }

        public void Unfreeze()
        {
            _movement.enabled = true;
            _beamShooter.enabled = true;
        }

        public void Disappear()
        {
            _player.SetActive(false);
            _enableStateEvents.Disable();
        }

        public void Appear()
        {
            _player.SetActive(true);
            _playerHealth.Revive();
            _enableStateEvents.Enable();
        }

        public void Kill()
        {
            _playerHealth.InstantKill();
        }

        public void Teleport(Vector3 position, bool keepMomentum = true)
        {
            _motor.SetPosition(position);

            if (!keepMomentum)
            {
                _motor.BaseVelocity = Vector3.zero;
            }
        }

        public void DisableBeamFor(float seconds)
        {
            CancelBeamTimer();
            _beamCts = new CancellationTokenSource();
            DisableBeamForAsync(seconds, _beamCts.Token).Forget();
        }

        private async UniTaskVoid DisableBeamForAsync(float seconds, CancellationToken token)
        {
            _beamShooter.enabled = false;
            await UniTask.Delay(System.TimeSpan.FromSeconds(seconds), cancellationToken: token);
            _beamShooter.enabled = true;
        }

        private void CancelBeamTimer()
        {
            _beamCts?.Cancel();
            _beamCts?.Dispose();
            _beamCts = null;
        }
    }
}
