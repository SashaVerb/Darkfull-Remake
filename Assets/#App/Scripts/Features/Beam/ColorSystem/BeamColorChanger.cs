using UnityEngine;

namespace Features.Beam.ColorSystem
{
    public class BeamColorChanger : MonoBehaviour
    {
        [field: SerializeField] public BeamColor Color { get; private set; }
    }
}