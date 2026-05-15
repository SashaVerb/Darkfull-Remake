using UnityEngine;

namespace Modules.Interactable
{
    [RequireComponent(typeof(Collider))]
    public class TriggerInteractable : Interactable
    {
        private int _overlapCount;

        private void OnTriggerEnter(Collider other)
        {
            _overlapCount++;

            if (IsActive)
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

        public override void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;
            OnActivate?.Invoke();
        }

        public override void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            OnDeactivate?.Invoke();
        }
    }
}
