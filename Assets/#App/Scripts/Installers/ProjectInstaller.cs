using _App.Scripts.Modules.LevelManagement;
using UIManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectInstaller : LifetimeScope
{
    [SerializeField] private UIPanel _transition;
    [SerializeField] private LevelConfig _levelConfig;
    
    override protected void Configure(IContainerBuilder builder)
    {
        builder.Register<LevelManager>(Lifetime.Singleton)
            .WithParameter(_transition)
            .WithParameter(_levelConfig);
    }
}
