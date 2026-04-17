using UnityEngine;

namespace _App.Scripts.Features.Death
{
    public class InstantKiller : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health))
                health.InstantKill();
        }
    }
}