using _App.Scripts.Features.Death;
using _App.Scripts.Features.Pause;
using _App.Scripts.Modules.Extensions.VContainer;
using Features.Beam.ColorSystem;
using Features.PlayerLogic;
using KinematicCharacterController;
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
        [SerializeField] private Transform _beamEnd;
        [SerializeField] private BeamColorPickerView _beamColorPickerView;
        [SerializeField] private KinematicCharacterMotor _motor;
        [SerializeField] private MonoBehaviour[] _pausableComponents;
        
        protected override void Configure(IContainerBuilder builder)
        {
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
            builder.RegisterInstance(_target).Keyed("Target");
            builder.RegisterInstance(_beamEnd).Keyed("BeamEnd");
            
            builder.RegisterInstance(_playerMovement);
            builder.RegisterInstance(_playerHealth);
            builder.RegisterInstance(_motor);
            builder.RegisterComponent(_beamShooter);

            builder.Register<IPausable, PausableComponents>(Lifetime.Scoped).WithParameter(_pausableComponents);
            
            builder.Register<PlayerFacade>(Lifetime.Singleton).WithParameter(_motor.gameObject);
        }
    }
}
