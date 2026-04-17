using UnityEngine;

namespace _App.Scripts.Features.Death
{
    public class SlowKiller : MonoBehaviour
    {
        [SerializeField] private float _damagePerSecond = 1f;

        private PlayerHealth _playerHealth;

        private void Update()
        {
            if (_playerHealth != null)
                _playerHealth.TakeDamage(_damagePerSecond * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health))
                _playerHealth = health;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health) && health == _playerHealth)
                _playerHealth = null;
        }
    }
}