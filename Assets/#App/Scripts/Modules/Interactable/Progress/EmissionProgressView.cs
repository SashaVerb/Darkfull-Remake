using DG.Tweening;
using Modules.Interaction;
using UnityEngine;

namespace Modules.Interactable.Progress
{
    [RequireComponent(typeof(Renderer))]
    public class EmissionProgressView : MonoBehaviour
    {
        static readonly int EMISSION_PROPERTY_ID = Shader.PropertyToID("_EmissionColor");
        const string EMISSION_TOGGLE_PROPERTY_NAME = "_EMISSION";
        
        [SerializeField] private InteractableWithProgress _progress;
        [ColorUsage(true, true)]
        [SerializeField] private Color _emissionColor = Color.white;
        
        private Renderer _renderer;
        private Material _material;
        private Tween _currentTween;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
            
            if (!_material.IsKeywordEnabled(EMISSION_TOGGLE_PROPERTY_NAME))
            {
                _material.EnableKeyword(EMISSION_TOGGLE_PROPERTY_NAME);
            }
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
            var lerpedColor = Color.Lerp(Color.black, _emissionColor, progress);
            _material.SetColor(EMISSION_PROPERTY_ID, lerpedColor);
        }

        private void OnValidate()
        {
            _progress = GetComponentInParent<InteractableWithProgress>();
        }
    }
}
