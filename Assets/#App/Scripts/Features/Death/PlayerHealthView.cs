using System;
using UIManagement;
using VContainer;

namespace _App.Scripts.Features.Death
{
    public class PlayerHealthView : UIPanel, IDisposable
    {
        private PlayerHealth _playerHealth;

        [Inject]
        private void Configure(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            
            _playerHealth.OnRevive.AddListener(Clear);
        }

        private void Clear()
        {
            CanvasGroupComponent.alpha = 0;
        }

        private void Update()
        {
            if(_playerHealth == null || _playerHealth.IsDead)
                return;
            
            float normalizedHp = _playerHealth.CurrentHp / _playerHealth.MaxHp;
            CanvasGroupComponent.alpha = 1 - normalizedHp;
        }

        public void Dispose()
        {
            if(_playerHealth != null)
                _playerHealth.OnRevive.RemoveListener(Clear);
            
            _playerHealth = null;
        }
    }
}
