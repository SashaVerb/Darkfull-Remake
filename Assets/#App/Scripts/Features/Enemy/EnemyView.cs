using UnityEngine;
using CharacterController = KinematicCharacterController.Examples.CharacterController;

public class EnemyView : MonoBehaviour
{
    private readonly int SpeedX = Animator.StringToHash("SpeedX");
    private readonly int SpeedY = Animator.StringToHash("SpeedY");
    private readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;

    private void Update()
    {
        float movementDirectionSign = Mathf.Sign(Vector3.Dot(_characterController.Velocity, transform.forward));
        float horizontalSpeed = Mathf.Abs(_characterController.Velocity.x) * movementDirectionSign;
        
        if (movementDirectionSign < 0)
        {
            transform.rotation = Quaternion.LookRotation(-transform.forward);
        }
        
        _animator.SetFloat(SpeedX, horizontalSpeed);
        _animator.SetFloat(SpeedY, _characterController.Velocity.y);
        _animator.SetBool(IsGrounded, _characterController.IsGrounded);
    }

    // private void OnAnimatorIK(int layerIndex)
    // {
    //     if (_animator != null && _target != null)
    //     {
    //         _animator.SetLookAtWeight(1f);
    //         _animator.SetLookAtPosition(_target.position);
    //     }
    // }

    public void Pause()
    {
        _animator.enabled = false;
        enabled = false;
    }

    public void Resume()
    {
        _animator.enabled = true;
        enabled = true;
    }
}
