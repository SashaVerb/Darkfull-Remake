using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class RotateProgressView : MonoBehaviour
    {
        [SerializeField] private Progress _progress;
        [SerializeField] private Vector3 _totalRotation;
        
        private Quaternion _baseRotation;

        private void Awake()
        {
            _baseRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            _baseRotation = transform.localRotation;
            _progress.OnProgressChange.AddListener(Apply);
            Apply(_progress.CurrentProgress);
        }

        private void OnDisable()
        {
            _progress.OnProgressChange.RemoveListener(Apply);
        }

        private void Apply(float progress)
        {
            transform.localRotation = _baseRotation * Quaternion.Euler(_totalRotation * progress);;
        }

        private void OnValidate()
        {
            _progress = GetComponentInParent<Progress>();
        }
    }
}