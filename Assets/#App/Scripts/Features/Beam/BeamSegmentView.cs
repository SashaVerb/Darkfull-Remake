using UnityEngine;

namespace Features.Beam
{
    public class BeamSegmentView : MonoBehaviour
    {
        [field: SerializeField] public MeshFilter Mesh;
        [field: SerializeField] public MeshRenderer Renderer;

        public Material Material
        {
            get => Renderer.sharedMaterial;
            set => Renderer.sharedMaterial = value;
        }
    }
}