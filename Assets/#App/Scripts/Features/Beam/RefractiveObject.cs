using UnityEngine;

namespace Features.Beam
{
    public class RefractiveObject : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float _ior = 1.5f;

        public float IOR => _ior;
    }
}
