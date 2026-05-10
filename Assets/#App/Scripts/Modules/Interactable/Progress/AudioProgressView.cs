using Modules.Interaction;
using UnityEngine;

namespace Modules.Interactable.Progress
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioProgressView : MonoBehaviour
    {
        [SerializeField] private InteractableWithProgress _progress;
        [SerializeField] private float _minVolume = 0f;
        [SerializeField] private float _maxVolume = 1f;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _progress.OnProgressChange += Apply;
            Apply(_progress.CurrentProgress);
        }

        private void OnDisable()
        {
            _progress.OnProgressChange -= Apply;
        }

        private void Apply(float progress)
        {
            _audioSource.volume = Mathf.Lerp(_minVolume, _maxVolume, progress);
        }

        private void OnValidate()
        {
            _progress = GetComponentInParent<InteractableWithProgress>();
        }
    }
}
