using KinematicCharacterController;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private KinematicCharacterMotor _motor;

    private readonly int _speedHash = Animator.StringToHash("Speed");

    private void Update()
    {
        float horizontalSpeed = Mathf.Abs(_motor.Velocity.x);
        _animator.SetFloat(_speedHash, horizontalSpeed);

        float sign = Vector3.Dot(_motor.Velocity, transform.forward);
        Debug.Log($"Motor: {_motor.Velocity}, transform.forward: {transform.forward}, sign: {sign}");
        if (sign < 0)
        {
            transform.rotation = Quaternion.LookRotation(-transform.forward);
        }
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
