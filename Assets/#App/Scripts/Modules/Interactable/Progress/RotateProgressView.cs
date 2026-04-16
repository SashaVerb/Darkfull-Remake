using Modules.Interaction;
using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class RotateProgressView : MonoBehaviour
    {
        [SerializeField] private InteractableWithProgress _progress;
        [SerializeField] private Vector3 _totalRotation;
        
        private Quaternion _baseRotation;
        private Quaternion _startRotation;

        private void Awake()
        {
            _baseRotation = transform.localRotation;
            _startRotation = _baseRotation * Quaternion.Euler(_totalRotation);
        }

        private void OnEnable()
        {
            _baseRotation = transform.localRotation;
            _startRotation = _baseRotation * Quaternion.Euler(_totalRotation);
            _progress.OnProgressChange += Apply;
            Apply(_progress.CurrentProgress);
        }

        private void OnDisable()
        {
            _progress.OnProgressChange -= Apply;
        }

        private void Apply(float progress)
        {
            transform.localRotation = _baseRotation * Quaternion.Euler(_totalRotation * progress);;
        }

        private void OnValidate()
        {
            _progress = GetComponentInParent<InteractableWithProgress>();
        }
    }
}