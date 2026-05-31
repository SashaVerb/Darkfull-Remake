using System;
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
        public event Action<Vector3> OnBeamPositionUpdated;
        
        [SerializeField] private BeamColor _startColor;
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private InputActionReference _showPickerAction;
        [SerializeField] private InputActionReference _scrollAction;
        
        public bool IsEmiting => !Mathf.Approximately(_pathCreator.ActiveDistanceLeft, 0f);

        public bool IsShooting { get; private set; }

        private Transform _target;
        private Transform _beamEnd;
        private BeamConfig _config;
        private BeamPathCreator _pathCreator;
        private BeamIndicatorSystem _beamIndicatorSystem;
        private BeamColorSystem _beamColorSystem;
        private BeamView _beamView;
        private BeamColorPickerView _colorPickerView;
        
        private List<BeamPathPoint> _pathPoints = new();
        private List<BeamColor> _pathColors = new();
        
        [Inject]
        private void Configure(BeamConfig config, BeamPathCreator pathCreator, BeamIndicatorSystem beamIndicatorSystem, 
            BeamColorSystem beamColorSystem, BeamView view, [Key("Target")] Transform target, [Key("BeamEnd")] Transform beamEnd, BeamColorPickerView picker)
        {
            _config = config;
            _pathCreator = pathCreator;
            _beamIndicatorSystem = beamIndicatorSystem;
            _beamColorSystem = beamColorSystem;
            _beamView = view;
            _target = target;
            _beamEnd = beamEnd;
            
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
            _scrollAction.action.performed += OnScroll;
            
            _colorPickerView.OnColorPicked += OnColorPicked;
        }

        private void OnDisable()
        {
            _shootAction.action.started -= OnShootStarted;
            _shootAction.action.canceled -= OnShootCanceled;
            _showPickerAction.action.started -= OnShowPickerPerformed;
            _showPickerAction.action.canceled -= OnHidePickerPerformed;
            _scrollAction.action.performed -= OnScroll;
            
            _colorPickerView.OnColorPicked -= OnColorPicked;

            _pathCreator.TargetDistanceLeft = 0f;
            _pathCreator.ActiveDistanceLeft = 0f;
            _pathCreator.DistanceReduce = 0f;
            StopBeam();
        }
        
        private void LateUpdate()
        {
            Vector3 direction = _target.position - transform.position;

            _pathPoints.Clear();
            _pathColors.Clear();
            _pathCreator.Emit(transform.position, direction, IsShooting, ref _pathPoints);
            if (_pathPoints.Count > 0)
            {
                _beamEnd.position = _pathPoints[_pathPoints.Count - 1].Position;
                OnBeamPositionUpdated?.Invoke(_pathPoints[_pathPoints.Count - 1].Position);
            }
            
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
        
        private void OnScroll(InputAction.CallbackContext ctx)
        {
            float scrollY = ctx.ReadValue<Vector2>().y;
            _pathCreator.DistanceReduce = Mathf.Clamp(
                _pathCreator.DistanceReduce - Mathf.Sign(scrollY) * _config.ScrollStep,
                0f,
                _config.MaxDistance);
        }

        private void OnShootStarted(InputAction.CallbackContext _) => StartBeam();

        private void StartBeam()
        {
            OnBeamStarted.Invoke();
            IsShooting = true;
        }

        private void OnShootCanceled(InputAction.CallbackContext _) => StopBeam();
        
        private void StopBeam()
        {
            OnBeamStopped.Invoke();
            IsShooting = false;
            _beamView.Clear();
            _beamIndicatorSystem.Clear();
        }
    }
}