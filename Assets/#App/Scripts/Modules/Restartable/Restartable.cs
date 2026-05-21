using UnityEngine;

namespace _App.Scripts.Modules.Restartable
{
    public class Restartable : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private Vector3 _initialLocalScale;
        private bool _initialActive;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SaveState();
        }

        private void SaveState()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _initialLocalScale = transform.localScale;
            _initialActive = gameObject.activeSelf;
        }

        public void ResetState()
        {
            gameObject.SetActive(_initialActive);

            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialLocalScale;

            if (_rigidbody != null)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
