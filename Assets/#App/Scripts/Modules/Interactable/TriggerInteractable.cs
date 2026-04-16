using UnityEngine;
using UnityEngine.Events;

namespace Modules.Interactable
{
    [RequireComponent(typeof(Collider))]
    public class TriggerInteractable : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public UnityEvent OnActivate { get; private set; }
        [field: SerializeField] public UnityEvent OnDeactivate { get; private set; }

        public bool IsActive => _isActive;

        private int _overlapCount;
        private bool _isActive;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null)
                col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _overlapCount++;

            if (_isActive)
                return;

            Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            _overlapCount = Mathf.Max(0, _overlapCount - 1);

            if (_overlapCount > 0)
                return;

            Deactivate();
        }

        public void Activate()
        {
            if (_isActive)
                return;

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
