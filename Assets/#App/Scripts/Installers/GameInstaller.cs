using _App.Scripts.Features.LevelLogic;
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
        
        private LifetimeScope _playerScope;
        
        protected override void Configure(IContainerBuilder builder)
        {
            SetSceneObjects(builder);
            
            builder.RegisterBuildCallback(BuildPlayer);
        }
        
        private void SetSceneObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
        }
        
        private void BuildPlayer(IObjectResolver obj)
        {
            var player = CreateChildFromPrefab(_playerPrefab, InstallLevelController);
            
            player.transform.SetParent(null);
            player.transform.position = _spawnPoint.position;
            player.transform.rotation = _spawnPoint.rotation;
        }

        private void InstallLevelController(IContainerBuilder builder)
        {
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<LoadingState>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelController>();
        }
    }
}