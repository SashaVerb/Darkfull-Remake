using UnityEngine;
using UnityEngine.Events;

namespace Modules.Interactable
{
    public class InstantInteractable : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public UnityEvent OnActivate { get; private set; }
        [field: SerializeField] public UnityEvent OnDeactivate { get; private set; }

        public bool IsActive => _isActive;

        private bool _isActive;

        public void Activate()
        {
            _isActive = true;
            OnActivate?.Invoke();
        }

        public void Deactivate()
        {
            if (!_isActive)
                return;

            _isActive = false;
            OnDeactivate?.Invoke();
        }
    }
}
