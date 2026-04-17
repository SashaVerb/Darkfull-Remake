using System;
using UnityEngine;

namespace Features.Beam
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BeamSegmentView : MonoBehaviour
    {
        [field: SerializeField] public MeshFilter Mesh;
        [field: SerializeField] public MeshRenderer Renderer;

        public Material Material
        {
            get => Renderer.sharedMaterial;
            set => Renderer.sharedMaterial = value;
        }

        private void OnValidate()
        {
            Mesh = GetComponent<MeshFilter>();
            Renderer = GetComponent<MeshRenderer>();
        }
    }
}