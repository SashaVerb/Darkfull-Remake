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
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_beamConfig);
            
            builder.Register<BeamPathCreator>(Lifetime.Singleton);
            builder.Register<BeamView>(Lifetime.Singleton).WithParameter(_beamParent);
            builder.Register<BeamIndicatorSystem>(Lifetime.Singleton);
            
            builder.RegisterInstance(_target);
        }
    }
}