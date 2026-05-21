using UnityEngine;

namespace Modules.Interactable.Progress
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioProgressView : MonoBehaviour
    {
        [SerializeField] private Progress _progress;
        [SerializeField] private float _minVolume = 0f;
        [SerializeField] private float _maxVolume = 1f;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _progress.OnProgressChange.AddListener(Apply);
            Apply(_progress.CurrentProgress);
        }

        private void OnDisable()
        {
            _progress.OnProgressChange.RemoveListener(Apply);
        }

        private void Apply(float progress)
        {
            if(progress > 0f && !_audioSource.isPlaying)
                _audioSource.Play();
            
            if(Mathf.Approximately(progress, 0f) && _audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.volume = Mathf.Lerp(_minVolume, _maxVolume, progress);
        }

        private void OnValidate()
        {
            _progress = GetComponentInParent<Progress>();
        }
    }
}
