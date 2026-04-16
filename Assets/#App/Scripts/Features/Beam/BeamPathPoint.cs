using UnityEngine;

namespace Features.Beam
{
    public struct BeamPathPoint
    {
        public Vector3 Position;
        public RaycastHit? Hit;
        
        public BeamPathPoint(Vector3 position)
        {
            Position = position;
            Hit = null;
        }
        
        public BeamPathPoint(Vector3 position, RaycastHit hit)
        {
            Position = position;
            Hit = hit;
        }
    }
}