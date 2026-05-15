using _App.Scripts.Modules.Interactable;
using Features.PlayerLogic;
using KinematicCharacterController;
using UnityEngine;
using VContainer;

namespace Modules.Interactable
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private InteractionContext _interactionContext;
        [SerializeField] private Interactable _interactable;
        [SerializeField] private Transform _overrideTeleportDestination;

        private Vector3 Destination => _overrideTeleportDestination != null ? _overrideTeleportDestination.position : transform.position;

        private void OnEnable()
        {
            _interactable.OnActivate.AddListener(Teleport);
        }

        private void OnDisable()
        {
            _interactable.OnActivate.RemoveListener(Teleport);
        }

        public void Teleport()
        {
            var resolver = _interactionContext.ObjectResolver;
            if (resolver == null)
                return;

            if (resolver.TryResolve<KinematicCharacterMotor>(out var motor))
            {
                motor.SetPosition(Destination);
            }

            if (resolver.TryResolve<PlayerFacade>(out var player))
            {
                player.DisableBeamFor(1f);
            }
        }

        private void OnValidate()
        {
            _interactionContext = GetComponentInParent<InteractionContext>();
            _interactable = GetComponentInParent<Interactable>();
        }
    }
}
