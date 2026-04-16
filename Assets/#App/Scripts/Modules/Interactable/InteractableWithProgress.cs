using System;
using Modules.Interactable;
using Modules.Interactable.Progress;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Interaction
{
    public class InteractableWithProgress : MonoBehaviour, IInteractable, IProgress
    {
        [SerializeField] private float _duration = 1f;

        [field: SerializeField] public UnityEvent OnActivate { get; private set; }
        [field: SerializeField] public UnityEvent OnDeactivate { get; private set; }
        
        public event Action<float> OnProgressChange;

        public bool IsActive { get; private set; }

        public float CurrentProgress => Mathf.Clamp01(_elapsed / _duration);

        private float _elapsed;
        private bool _isActivating;

        private void Update()
        {
            if (_isActivating)
            {
                _elapsed += Time.deltaTime;
                _elapsed = Mathf.Min(_duration, _elapsed);
                
                float progress = CurrentProgress;
                OnProgressChange?.Invoke(progress);

                if (progress >= 1f && !IsActive)
                {
                    IsActive = true;
                    OnActivate.Invoke();
                }
            }
            else
            {
                _elapsed -= Time.deltaTime;
                _elapsed = Mathf.Max(0f, _elapsed);

                OnProgressChange?.Invoke(CurrentProgress);
            }
        }

        public void Activate()
        {
            if (_isActivating)
                return;
            
            _isActivating = true;
        }

        public void Deactivate()
        {
            if (!_isActivating)
                return;

            _isActivating = false;

            if (IsActive)
            {
                IsActive = false;
                OnDeactivate.Invoke();
            }
        }
    }
}
