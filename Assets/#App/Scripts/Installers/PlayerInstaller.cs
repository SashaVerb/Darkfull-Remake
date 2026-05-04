using _App.Scripts.Features.Death;
using _App.Scripts.Modules.Extensions.VContainer;
using Features.Beam.ColorSystem;
using Features.PlayerLogic;
using KinematicCharacterController.Examples;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Installers
{
    public class PlayerInstaller : LifetimeScope, IInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private BeamShooter _beamShooter;
        [SerializeField] private BeamConfig _beamConfig;
        [SerializeField] private Transform _beamParent;
        [SerializeField] private Transform _target;
        [SerializeField] private BeamColorPickerView _beamColorPickerView;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("Configuring PlayerInstaller");
            Install(builder);
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_beamConfig);
            
            builder.Register<BeamPathCreator>(Lifetime.Singleton);
            builder.Register<BeamView>(Lifetime.Singleton).WithParameter(_beamParent);
            builder.Register<BeamIndicatorSystem>(Lifetime.Singleton);
            builder.Register<BeamColorSystem>(Lifetime.Singleton);
            
            builder.RegisterUI(_beamColorPickerView);
            builder.RegisterInstance(_target);
            
            builder.RegisterInstance(_playerMovement);
            builder.RegisterInstance(_playerHealth);
        }
    }
}
