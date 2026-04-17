using UnityEngine;

namespace KinematicCharacterController.Examples
{
    public class EnemyMovement : MonoBehaviour
    {
        public CharacterController Character;
        public Transform Target;
        public float StopDistance;
        public float ChaseDistance;

        private void Update()
        {
            HandleCharacterInput();
        }

        private void HandleCharacterInput()
        {
            AICharacterInputs characterInputs = new AICharacterInputs();

            if (Character == null || Target == null)
            {
                characterInputs.MoveVector = Vector3.zero;
                characterInputs.LookVector = Vector3.zero;
                Character?.SetInputs(ref characterInputs);
                return;
            }

            Vector3 targetDirection = Target.position - transform.position;
            targetDirection = Vector3.ProjectOnPlane(targetDirection, Character.Motor.CharacterUp);

            if (targetDirection.sqrMagnitude <= StopDistance * StopDistance || targetDirection.sqrMagnitude >= ChaseDistance * ChaseDistance)
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
