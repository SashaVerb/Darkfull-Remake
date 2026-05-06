using _App.Scripts.Features.LevelLogic;
using _App.Scripts.Modules.Extensions.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerInstaller _playerPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        protected override void Configure(IContainerBuilder builder)
        {
            SetSceneObjects(builder);
            
            builder.RegisterPrefabInstaller(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);
            
            InstallLevelController(builder);
        }
        
        private void SetSceneObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
        }
        
        private void InstallLevelController(IContainerBuilder builder)
        {
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<LoadingState>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelController>();
        }
    }
}