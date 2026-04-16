using System.Collections.Generic;
using Features.Beam;
using Features.Beam.ColorSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Features.PlayerLogic
{
    public class BeamShooter : MonoBehaviour
    {
        [SerializeField] private BeamColor _startColor;
        [SerializeField] private InputActionReference _shootAction;
        
        public bool IsShooting => _isShooting;
        
        private Transform _target;
        private BeamPathCreator _pathCreator;
        private BeamIndicatorSystem _beamIndicatorSystem;
        private BeamColorSystem _beamColorSystem;
        private BeamView _view;
        
        private bool _isShooting;
        private List<BeamPathPoint> points;
        private List<BeamColor> pointsColor;
        
        [Inject]
        private void Configure(BeamPathCreator pathCreator, BeamIndicatorSystem beamIndicatorSystem, 
            BeamColorSystem beamColorSystem, BeamView view, Transform target)
        {
            _pathCreator = pathCreator;
            _beamIndicatorSystem = beamIndicatorSystem;
            _beamColorSystem = beamColorSystem;
            _view = view;
            _target = target;
            
            points = new List<BeamPathPoint>();
            pointsColor = new List<BeamColor>();
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
            
            points.Clear();
            pointsColor.Clear();
            _pathCreator.Emit(transform.position, direction, ref points);
            _beamIndicatorSystem.Check(points);
            _beamColorSystem.GetColors(points, _startColor, ref pointsColor);
            _view.Display(points, pointsColor);
        }

        private void OnShootStarted(InputAction.CallbackContext _) => _isShooting = true;

        private void OnShootCanceled(InputAction.CallbackContext _) => StopBeam();

        private void StopBeam()
        {
            _isShooting = false;
            _view.Clear();
            _beamIndicatorSystem.Clear();
        }
    }
}