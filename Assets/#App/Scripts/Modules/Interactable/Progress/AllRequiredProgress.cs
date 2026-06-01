using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class AllRequiredProgress : Progress
    {
        [SerializeField] private Interactable[] _interactables;

        private void OnEnable()
        {
            foreach (var interactable in _interactables)
            {
                interactable.OnActivate.AddListener(OnStateChanged);
                interactable.OnDeactivate.AddListener(OnStateChanged);
            }

            OnStateChanged();
        }

        private void OnDisable()
        {
            foreach (var interactable in _interactables)
            {
                interactable.OnActivate.RemoveListener(OnStateChanged);
                interactable.OnDeactivate.RemoveListener(OnStateChanged);
            }
        }

        private void OnStateChanged()
        {
            if (_interactables.Length == 0)
                return;

            int activeCount = 0;
            foreach (var interactable in _interactables)
            {
                if (interactable.IsActive)
                    activeCount++;
            }

            float target = (float)activeCount / _interactables.Length;

            if (Mathf.Approximately(target, CurrentProgress))
                return;

            float prev = CurrentProgress;
            CurrentProgress = target;
            OnProgressChange.Invoke(CurrentProgress);

            if (CurrentProgress >= 1f && prev < 1f)
                OnProgressMax.Invoke(CurrentProgress);
            else if (CurrentProgress <= 0f && prev > 0f)
                OnProgressMin.Invoke(CurrentProgress);
        }
    }
}
