using Modules.MouseFollower;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.PlayerLogic
{
    public class BeamShooter : MonoBehaviour
    {
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private BeamPathCreator _pathCreator;
        [SerializeField] private IndicatorDetector _indicatorDetector;
        [SerializeField] private BeamView _view;
        [SerializeField] private MouseWorldFollower _mouseFollower;

        private bool _isShooting;

        private void OnEnable()
        {
            _shootAction.action.started += OnShootStarted;
            _shootAction.action.canceled += OnShootCanceled;
        }

        private void OnDisable()
        {
            _shootAction.action.started -= OnShootStarted;
            _shootAction.action.canceled -= OnShootCanceled;
            StopBeam();
        }
        
        private void Update()
        {
            if (!_isShooting)
                return;

            Vector3 direction = _mouseFollower.transform.position - transform.position;

            if (direction.sqrMagnitude < 0.001f)
                return;

            var points = _pathCreator.Emit(transform.position, direction);
            _indicatorDetector.Check(points);
            _view.Display(points);
        }

        private void OnShootStarted(InputAction.CallbackContext _) => _isShooting = true;

        private void OnShootCanceled(InputAction.CallbackContext _) => StopBeam();

        private void StopBeam()
        {
            _isShooting = false;
            _view.Clear();
            _indicatorDetector.Clear();
        }
    }
}