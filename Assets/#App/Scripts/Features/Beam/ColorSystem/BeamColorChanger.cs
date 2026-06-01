using UnityEngine;

namespace Features.Beam.ColorSystem
{
    public class BeamColorChanger : MonoBehaviour
    {
        [field: SerializeField] public BeamColor Color { get; private set; }
        
        [SerializeField] private BeamConfig _config;
        
        private void OnValidate()
        {
            if (_config == null) return;

            var material = _config.GetMaterialForColor(Color);

            if (material == null) return;

            GetComponentInChildren<Renderer>().sharedMaterial = material;
        }
    }
}