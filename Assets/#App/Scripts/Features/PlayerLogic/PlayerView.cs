using Features.PlayerLogic;
using UnityEngine;
using CharacterController = KinematicCharacterController.Examples.CharacterController;

public class PlayerView : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("Jump");
    private readonly int SpeedX = Animator.StringToHash("SpeedX");
    private readonly int SpeedY = Animator.StringToHash("SpeedY");
    private readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _target;
    [SerializeField] private BeamShooter _beamShooter;

    private void Update()
    {
        float targetDirectionSign = Vector3.Dot(_target.position - transform.position, transform.forward);
        if (targetDirectionSign < 0)
        {
            transform.rotation = Quaternion.LookRotation(-transform.forward);
        }

        float movementDirectionSign = Mathf.Sign(Vector3.Dot(_characterController.Velocity, transform.forward));
        float horizontalSpeed = Mathf.Abs(_characterController.Velocity.x) * movementDirectionSign;

        _animator.SetFloat(SpeedX, horizontalSpeed);
        _animator.SetFloat(SpeedY, _characterController.Velocity.y);
        _animator.SetBool(IsGrounded, _characterController.IsGrounded);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator != null && _target != null)
        {
            if (_beamShooter.IsShooting)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _target.position);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _target.position);
            }
            
            _animator.SetLookAtWeight(1f);
            _animator.SetLookAtPosition(_target.position);
        }
    }

    private void OnEnable()
    {
        _characterController.OnJump += PlayJumpAnimation;
    }
    
    private void OnDisable()
    {
        _characterController.OnJump -= PlayJumpAnimation;
    }

    private void PlayJumpAnimation()
    {
        _animator.SetTrigger(Jump);        
    }

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
