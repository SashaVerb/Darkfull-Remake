using _App.Scripts.Modules.Interactable;
using Features.PlayerLogic;
using UnityEngine;
using VContainer;

namespace Modules.MouseFollower
{
    [RequireComponent(typeof(InteractionContext))]
    public class BeamEndFollower : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private InteractionContext _context;
        [SerializeField] private Collider _boundsCollider;

        private BeamShooter _beamShooter;

        private void UpdatePosition(Vector3 position)
        {
            if (!_beamShooter.IsShooting)
                return;
            
            Vector3 destination = ApplyBounds(position);

            Physics.BoxCast(transform.position, _collider.bounds.extents, destination - transform.position,
                out RaycastHit hitInfo, Quaternion.identity, (destination - transform.position).magnitude);

            if (hitInfo.collider != null)
            {
                transform.position = hitInfo.point;
            }
            else
            {
                transform.position = destination;
            }
            Physics.SyncTransforms();
        }
        
        private void OnEnable()
        {
            if (_context.ObjectResolver != null)
            {
                _context.ObjectResolver.TryResolve(out _beamShooter);
                
                _beamShooter.OnBeamPositionUpdated += UpdatePosition;
            }
        }

        private void OnDisable()
        {
            if(_beamShooter != null)
                _beamShooter.OnBeamPositionUpdated -= UpdatePosition;

            _beamShooter = null;
        }

        private Vector3 ApplyBounds(Vector3 position)
        {
            if (_boundsCollider == null)
                return position;

            return _boundsCollider.ClosestPoint(position);
        }
    }
}
