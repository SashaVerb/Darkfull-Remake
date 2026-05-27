using System.Collections.Generic;
using Features.Beam;
using Features.Beam.ColorSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using VContainer;

namespace Features.PlayerLogic
{
    public class BeamShooter : MonoBehaviour
    {
        public UnityEvent OnBeamStarted;
        public UnityEvent OnBeamStopped;
        
        [SerializeField] private BeamColor _startColor;
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private InputActionReference _showPickerAction;
        
        public bool IsShooting => !Mathf.Approximately(_pathCreator.DistanceLeft, 0f);
        
        private Transform _target;
        private BeamPathCreator _pathCreator;
        private BeamIndicatorSystem _beamIndicatorSystem;
        private BeamColorSystem _beamColorSystem;
        private BeamView _beamView;
        private BeamColorPickerView _colorPickerView;
        private bool _isShooting;
        
        private List<BeamPathPoint> _pathPoints = new();
        private List<BeamColor> _pathColors = new();
        
        [Inject]
        private void Configure(BeamPathCreator pathCreator, BeamIndicatorSystem beamIndicatorSystem, 
            BeamColorSystem beamColorSystem, BeamView view, Transform target, BeamColorPickerView picker)
        {
            _pathCreator = pathCreator;
            _beamIndicatorSystem = beamIndicatorSystem;
            _beamColorSystem = beamColorSystem;
            _beamView = view;
            _target = target;

            _colorPickerView = picker;
        }

        private void OnColorPicked(BeamColor obj)
        {
            _startColor = obj;
        }

        private void OnEnable()
        {
            _shootAction.action.started += OnShootStarted;
            _shootAction.action.canceled += OnShootCanceled;
            _showPickerAction.action.started += OnShowPickerPerformed;
            _showPickerAction.action.canceled += OnHidePickerPerformed;
            
            _colorPickerView.OnColorPicked += OnColorPicked;
        }

        private void OnDisable()
        {
            _shootAction.action.started -= OnShootStarted;
            _shootAction.action.canceled -= OnShootCanceled;
            _showPickerAction.action.started -= OnShowPickerPerformed;
            _showPickerAction.action.canceled -= OnHidePickerPerformed;
            
            _colorPickerView.OnColorPicked -= OnColorPicked;

            _pathCreator.DistanceLeft = 0f;
            StopBeam();
        }
        
        private void LateUpdate()
        {
            Vector3 direction = _target.position - transform.position;

            _pathPoints.Clear();
            _pathColors.Clear();
            _pathCreator.Emit(transform.position, direction, _isShooting, ref _pathPoints);
            _beamColorSystem.GetColors(_pathPoints, _startColor, ref _pathColors);
            _beamIndicatorSystem.Check(_pathPoints, _pathColors);
            _beamView.Display(_pathPoints, _pathColors);
        }
        
        private void OnShowPickerPerformed(InputAction.CallbackContext _)
        {
            _colorPickerView.Show();
        }
        
        private void OnHidePickerPerformed(InputAction.CallbackContext _)
        {
            _colorPickerView.Hide();
        }
        
        private void OnShootStarted(InputAction.CallbackContext _) => StartBeam();

        private void StartBeam()
        {
            OnBeamStarted.Invoke();
            _isShooting = true;
        }

        private void OnShootCanceled(InputAction.CallbackContext _) => StopBeam();
        
        private void StopBeam()
        {
            OnBeamStopped.Invoke();
            _isShooting = false;
            _beamView.Clear();
            _beamIndicatorSystem.Clear();
        }
    }
}