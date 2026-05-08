using _App.Scripts.Modules.LevelManagement;
using UILogic;
using UIManagement;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

public class ProjectInstaller : LifetimeScope
{
    [SerializeField] private UIPanel _transition;
    [SerializeField] private LevelConfig _levelConfig;
    [Space]
    [SerializeField] private AudioMixer _audioMixer;
    
    override protected void Configure(IContainerBuilder builder)
    {
        builder.Register<LevelManager>(Lifetime.Singleton)
            .WithParameter(
                UIManager.Instantiate(_transition, UISortGroup.TopLevel)
                )
            .WithParameter(_levelConfig);
        
        builder.Register<SoundPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
            .WithParameter(_audioMixer);
    }
}
