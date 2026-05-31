using KinematicCharacterController;
using UnityEngine;
using UnityEngine.Events;

namespace _App.Scripts.Modules.Restartable
{
    public class Restartable : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private Vector3 _initialLocalScale;
        private bool _initialActive;

        private Rigidbody _rigidbody;
        private KinematicCharacterMotor _kinematicCharacterMotor;
        
        public UnityEvent OnReset;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _kinematicCharacterMotor = GetComponent<KinematicCharacterMotor>();
            SaveState();
        }

        private void SaveState()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _initialLocalScale = transform.localScale;
            _initialActive = gameObject.activeSelf;
        }

        [ContextMenu("Reset State")]
        public void ResetState()
        {
            gameObject.SetActive(_initialActive);

            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialLocalScale;

            if (_rigidbody != null && !_rigidbody.isKinematic)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }

            if (_kinematicCharacterMotor != null)
            {
                _kinematicCharacterMotor.SetPosition(_initialPosition);
                _kinematicCharacterMotor.BaseVelocity = Vector3.zero;
            }
            
            OnReset.Invoke();
        }
    }
}
