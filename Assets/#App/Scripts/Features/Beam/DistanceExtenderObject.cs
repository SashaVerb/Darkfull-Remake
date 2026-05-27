using UnityEngine;

namespace Features.Beam
{
    public class DistanceExtenderObject : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _bonusDistance = 5f;

        public float BonusDistance => _bonusDistance;
    }
}
