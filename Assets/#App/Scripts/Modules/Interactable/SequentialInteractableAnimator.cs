using Modules.Interactable.Progress;
using UnityEngine;

namespace _App.Scripts.Modules.Interactable
{
    public class SequentialInteractableAnimator : MonoBehaviour
    {
        [SerializeField] private Progress _progress;
        [SerializeField] private InteractableAnimator[] _animators;

        private void OnEnable()
        {
            _progress.OnProgressChange.AddListener(OnProgressChanged);
            OnProgressChanged(_progress.CurrentProgress);
        }

        private void OnDisable()
        {
            _progress.OnProgressChange.RemoveListener(OnProgressChanged);
        }

        private void OnProgressChanged(float progress)
        {
            int activeCount = Mathf.RoundToInt(progress * _animators.Length);

            for (int i = 0; i < _animators.Length; i++)
            {
                if (i < activeCount)
                    _animators[i].Activate();
                else
                    _animators[i].Deactivate();
            }
        }
    }
}
