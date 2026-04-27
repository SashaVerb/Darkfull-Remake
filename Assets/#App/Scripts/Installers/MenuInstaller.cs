using _App.Scripts.Modules.Extensions.VContainer;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class MenuInstaller : LifetimeScope
{
    [SerializeField] private MenuView _menuView;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<MenuController>();
        builder.RegisterUI(_menuView);
    }
}
