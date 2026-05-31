using UnityEngine;

namespace _App.Scripts.Features.Death
{
    public class SlowKiller : MonoBehaviour
    {
        [SerializeField] private float _damagePerSecond = 1f;

        private PlayerHealth _playerHealth;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health))
            {
                health.TakeDamage(_damagePerSecond * Time.deltaTime);
            }
        }
    }
}