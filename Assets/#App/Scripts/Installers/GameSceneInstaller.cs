using _App.Scripts.Modules.Extensions.VContainer;
using Features.Beam.ColorSystem;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Installers
{
    public class GameSceneInstaller : LifetimeScope
    {
        [SerializeField] private BeamConfig _beamConfig;
        [SerializeField] private Transform _beamParent;
        [SerializeField] private Transform _target;
        [SerializeField] private BeamColorPickerView _beamColorPickerView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_beamConfig);
            
            builder.Register<BeamPathCreator>(Lifetime.Singleton);
            builder.Register<BeamView>(Lifetime.Singleton).WithParameter(_beamParent);
            builder.Register<BeamIndicatorSystem>(Lifetime.Singleton);
            builder.Register<BeamColorSystem>(Lifetime.Singleton);
            
            builder.RegisterUI(_beamColorPickerView);
            
            builder.RegisterInstance(_target);
        }
    }
}