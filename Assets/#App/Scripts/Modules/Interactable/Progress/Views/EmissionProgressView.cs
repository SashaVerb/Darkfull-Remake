using System.Linq;
using UnityEngine;

namespace Modules.Interactable.Progress
{
    public class EmissionProgressView : MonoBehaviour
    {
        static readonly int EMISSION_PROPERTY_ID = Shader.PropertyToID("_EmissionColor");
        const string EMISSION_TOGGLE_PROPERTY_NAME = "_EMISSION";
        
        [SerializeField] private Progress _progress;
        [ColorUsage(true, true)]
        [SerializeField] private Color _emissionColor = Color.white;
        [SerializeField] private Renderer[] _renderers;

        private Material[] _materials;

        private void Awake()
        {
            _materials = _renderers.Select(renderer => renderer.material).ToArray();

            foreach (var material in _materials)
            {
                if (!material.IsKeywordEnabled(EMISSION_TOGGLE_PROPERTY_NAME))
                {
                    material.EnableKeyword(EMISSION_TOGGLE_PROPERTY_NAME);
                }
            }
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
            var lerpedColor = Color.Lerp(Color.black, _emissionColor, progress);
            
            foreach (var material in _materials)
            {
                material.SetColor(EMISSION_PROPERTY_ID, lerpedColor);
            }
        }

        private void OnValidate()
        {
            if(_progress == null)
                _progress = GetComponentInParent<Progress>();
            
            if(_renderers == null)
                _renderers = GetComponentsInChildren<Renderer>();
        }

        [ContextMenu("Get Renderers")]
        private void GetRenderers()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }
    }
}
