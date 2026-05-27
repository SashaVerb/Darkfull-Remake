using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class CompositeProgress : Progress
    {
        [field: SerializeField] private Progress[] _progresses { get; set; }

        private void OnEnable()
        {
            CurrentProgress = CurrentProgress;
            foreach (var progress in _progresses)
            {
                progress.OnProgressChange.AddListener(CalculateProgressChange);
            }
        }

        private void CalculateProgressChange(float _)
        {
            float currentProgress = 0f;
            foreach (var progress in _progresses)
            {
                currentProgress = Mathf.Max(progress.CurrentProgress, currentProgress);
            }

            if (!Mathf.Approximately(currentProgress, CurrentProgress))
            {
                CurrentProgress = currentProgress;
                OnProgressChange.Invoke(CurrentProgress);

                if (CurrentProgress >= 1f)
                {
                    OnProgressMax.Invoke(CurrentProgress);
                }
                else if(CurrentProgress <= 0f)
                {
                    OnProgressMin.Invoke(CurrentProgress);
                }
            }
        }
    }
}