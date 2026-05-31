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
        [SerializeField] private bool _freezeX;
        [SerializeField] private bool _freezeY;
        [SerializeField] private bool _freezeZ;

        private BeamShooter _beamShooter;

        private void UpdatePosition(Vector3 position)
        {
            if (!_beamShooter.IsShooting)
                return;
            
            Vector3 destination = ApplyBounds(position);
            destination = ApplyAxisFreeze(destination);
            destination = ApplyAxisFreeze(destination);

            Physics.BoxCast(transform.position, _collider.bounds.extents, destination - transform.position,
                out RaycastHit hitInfo, Quaternion.identity, (destination - transform.position).magnitude);

            if (hitInfo.collider != null)
            {
                destination = hitInfo.point + (_collider.bounds.center - _collider.ClosestPoint(hitInfo.point));
            }
            
            destination = ApplyAxisFreeze(destination);
            transform.position = destination;
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

        private Vector3 ApplyAxisFreeze(Vector3 position)
        {
            return new Vector3(
                _freezeX ? transform.position.x : position.x,
                _freezeY ? transform.position.y : position.y,
                _freezeZ ? transform.position.z : position.z
            );
        }
    }
}
