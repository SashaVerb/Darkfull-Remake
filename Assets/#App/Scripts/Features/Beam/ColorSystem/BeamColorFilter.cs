using UnityEngine;

namespace Features.Beam.ColorSystem
{
    public class BeamColorFilter : MonoBehaviour
    {
        [field: SerializeField] public BeamColor RequiredColor { get; private set; }

        public bool AcceptsColor(BeamColor color) => color == RequiredColor;
    }
}
