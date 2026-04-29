using _App.Scripts.Modules.LevelManagement;
using UILogic;
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
            .WithParameter(
                UIManager.Instantiate(_transition, UISortGroup.TopLevel)
                )
            .WithParameter(_levelConfig);
    }
}
