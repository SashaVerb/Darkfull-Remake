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
        [SerializeField] private InputActionReference _showPickerAction;
        
        public bool IsShooting => _isShooting;
        
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

            StopBeam();
        }
        
        private void LateUpdate()
        {
            if (!_isShooting)
                return;

            Vector3 direction = _target.position - transform.position;

            if (direction.sqrMagnitude < 0.001f)
                return;
            
            _pathPoints.Clear();
            _pathColors.Clear();
            _pathCreator.Emit(transform.position, direction, ref _pathPoints);
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
        
        private void OnShootStarted(InputAction.CallbackContext _) => _isShooting = true;

        private void OnShootCanceled(InputAction.CallbackContext _) => StopBeam();

        private void StopBeam()
        {
            _isShooting = false;
            _beamView.Clear();
            _beamIndicatorSystem.Clear();
        }
    }
}