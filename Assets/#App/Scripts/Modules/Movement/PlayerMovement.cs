using UnityEngine;
using UnityEngine.InputSystem;

namespace KinematicCharacterController.Examples
{
    public class PlayerMovement : MonoBehaviour
    {
        public InputActionReference Jump;
        public InputActionReference Move;
        public KinematicCharacterController Character;
    
        private bool _jumpDown;
        private bool _crouchDown;
        private bool _crouchUp;

        private void OnEnable()
        {
            Jump.action.started += OnJumpStarted;
        }

        private void OnDisable()
        {
            Jump.action.started -= OnJumpStarted;
        }

        private void OnJumpStarted(InputAction.CallbackContext ctx) => _jumpDown = true;

        private void Update()
        {
            HandleCharacterInput();
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            Vector2 moveInput = Move.action.ReadValue<Vector2>();
            characterInputs.MoveAxisForward = moveInput.y;
            characterInputs.MoveAxisRight = moveInput.x;
            characterInputs.JumpDown = _jumpDown;
            characterInputs.CrouchDown = _crouchDown;
            characterInputs.CrouchUp = _crouchUp;

            Character.SetInputs(ref characterInputs);

            _jumpDown = false;
            _crouchDown = false;
            _crouchUp = false;
        }
    }
}
