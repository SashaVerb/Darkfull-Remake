using Features.Beam.ColorSystem;
using UnityEngine;

namespace Features.LevelElements
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialBindToColor : MonoBehaviour
    {
        [SerializeField] private BeamConfig _config;
        [SerializeField] private BeamColor _color;

        private void OnValidate()
        {
            if (_config == null) return;

            var material = _config.GetPropsMaterialForColor(_color);
            if (material == null) return;

            GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}
