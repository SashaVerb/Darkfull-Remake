using System;
using KinematicCharacterController;
using UnityEngine;
using VContainer;

namespace Modules.Interactable
{
    public class Teleporter : MonoBehaviour, IInteractionContext
    {
        [SerializeField] private Transform _overrideTeleportDestination;

        private Vector3 Destination => _overrideTeleportDestination != null ? _overrideTeleportDestination.position : transform.position;
        private IInteractable _interactable;
        private KinematicCharacterMotor _motor;
        
        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.OnActivate.AddListener(Teleport);
            _interactable.OnDeactivate.AddListener(Revoke);
        }

        private void OnDisable()
        {
            _interactable.OnActivate.RemoveListener(Teleport);
            _interactable.OnDeactivate.RemoveListener(Revoke);
        }

        public void Provide(IObjectResolver resolver)
        {
            _motor = resolver.Resolve<KinematicCharacterMotor>();
        }

        public void Revoke()
        {
            _motor = null;
        }

        private void Teleport()
        {
            _motor.SetPosition(Destination);
        }
    }
}
