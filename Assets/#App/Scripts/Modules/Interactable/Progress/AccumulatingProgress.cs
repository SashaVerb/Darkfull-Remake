using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class AccumulatingProgress : Progress
    {
        [SerializeField] private float _duration = 1f;

        public override float CurrentProgress => Mathf.Clamp01(_elapsed / _duration);

        private float _elapsed;
        private float _prevProgress;
        private bool _isAccumulating;

        private void Update()
        {
            if (_isAccumulating)
            {
                _elapsed += Time.deltaTime;
                _elapsed = Mathf.Min(_duration, _elapsed);
            }
            else
            {
                _elapsed -= Time.deltaTime;
                _elapsed = Mathf.Max(0f, _elapsed);
            }

            float progress = CurrentProgress;
            OnProgressChange?.Invoke(progress);

            if (progress >= 1f && _prevProgress < 1f)
                OnProgressMax?.Invoke(progress);
            else if (progress <= 0f && _prevProgress > 0f)
                OnProgressMin?.Invoke(progress);

            _prevProgress = progress;
        }

        public void Accumulate()
        {
            if (_isAccumulating)
                return;

            _isAccumulating = true;
        }

        public void RollBack()
        {
            if (!_isAccumulating)
                return;

            _isAccumulating = false;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Bind To Interactable")]
        private void BindToInteractable()
        {
            var interactable = GetComponentInParent<Interactable>();
            
            if (interactable == null)
                return;
            
            UnityEditor.Events.UnityEventTools.AddPersistentListener(interactable.OnActivate, Accumulate);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(interactable.OnDeactivate, RollBack);
            
            UnityEditor.EditorUtility.SetDirty(interactable);
        }
        #endif
    }
}
