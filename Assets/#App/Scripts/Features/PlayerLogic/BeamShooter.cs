using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Features.PlayerLogic
{
    public class BeamShooter : MonoBehaviour
    {
        [SerializeField] private InputActionReference _shootAction;
        
        public bool IsShooting => _isShooting;
        
        private Transform _target;
        private BeamPathCreator _pathCreator;
        private IndicatorDetector _indicatorDetector;
        private BeamView _view;
        
        private bool _isShooting;
        
        [Inject]
        private void Configure(BeamPathCreator pathCreator, IndicatorDetector indicatorDetector, 
            BeamView view, Transform target)
        {
            _pathCreator = pathCreator;
            _indicatorDetector = indicatorDetector;
            _view = view;
            _target = target;
        }
        
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

            Vector3 direction = _target.position - transform.position;

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