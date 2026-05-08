using System.Collections.Generic;
using UnityEngine;

namespace KinematicCharacterController.Examples
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController Character;
        
        public float StopDistance = 1.5f;

        private readonly List<Transform> _targets = new();
        private Transform _currentTarget;

        private void OnTriggerEnter(Collider other)
        {
            _targets.Add(other.transform);
            _currentTarget = other.transform;
        }

        private void OnTriggerExit(Collider other)
        {
            _targets.Remove(other.transform);
            if (other.transform == _currentTarget)
            {
                _currentTarget = _targets.Count > 0 ? _targets[^1] : null;
            }
        }

        public void Stop()
        {
            AICharacterInputs characterInputs = new AICharacterInputs();
        
            characterInputs.MoveVector = Vector3.zero;
            characterInputs.LookVector = Vector3.zero;
            Character?.SetInputs(ref characterInputs);
        }
        
        private void Update()
        {
            HandleCharacterInput();
        }

        private void HandleCharacterInput()
        {
            AICharacterInputs characterInputs = new AICharacterInputs();

            if (_currentTarget == null)
            {
                characterInputs.MoveVector = Vector3.zero;
                characterInputs.LookVector = Vector3.zero;
                Character?.SetInputs(ref characterInputs);
                return;
            }

            Vector3 targetDirection = _currentTarget.position - transform.position;
            targetDirection = Vector3.ProjectOnPlane(targetDirection, Character.Motor.CharacterUp);

            if (targetDirection.sqrMagnitude <= StopDistance * StopDistance)
            {
                characterInputs.MoveVector = Vector3.zero;
                characterInputs.LookVector = Vector3.zero;
            }
            else
            {
                Vector3 moveDirection = targetDirection.normalized;
                characterInputs.MoveVector = moveDirection;
                characterInputs.LookVector = moveDirection;
            }

            Character.SetInputs(ref characterInputs);
        }
    }
}
