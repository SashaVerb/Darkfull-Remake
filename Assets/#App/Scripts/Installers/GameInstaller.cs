using System.Collections.Generic;
using _App.Scripts.Features.LevelLogic;
using _App.Scripts.Features.Pause;
using _App.Scripts.Modules.Extensions.VContainer;
using Features.PlayerLogic;
using KinematicCharacterController;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Installers
{
    public class GameInstaller : LifetimeScope
    {
        [Header("LevelObjects")]
        [SerializeField] private LevelComplete _levelComplete;
        [Header("PauseMenu")]
        [SerializeField] private PauseView pauseViewPrefab;
        [SerializeField] private InputActionReference _pauseAction;
        [Header("Camera")]
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private bool _cameraFollowsPlayer;
        [Space]
        [Header("Player")]
        [SerializeField] private PlayerInstaller _playerPrefab;
        [SerializeField] private List<Transform> _spawnPoints;
        
        protected override void Configure(IContainerBuilder builder)
        {
            SetSceneObjects(builder);
            
            SetPlayer(builder);

            InstallLevelController(builder);
            
            SetPauseMenu(builder);
            
            builder.RegisterBuildCallback(SetupCamera);
        }
        
        private void SetSceneObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_cinemachineCamera);
            builder.RegisterInstance(_levelComplete);
        }

        private void SetPlayer(IContainerBuilder builder)
        {
            builder.RegisterInstance(new PlayerSpawn(_spawnPoints));
            builder.RegisterPrefabInstaller(_playerPrefab, _spawnPoints[0].position, _spawnPoints[0].rotation);
        }

        private void InstallLevelController(IContainerBuilder builder)
        {
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<LoadingState>(Lifetime.Singleton);
            builder.Register<DeathState>(Lifetime.Singleton);
            builder.Register<LevelCompleteState>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelController>();
        }

        private void SetPauseMenu(IContainerBuilder builder)
        {
            builder.RegisterUI(pauseViewPrefab).NonLazy();
            builder.RegisterEntryPoint<PausePresenter>().AsSelf().WithParameter(_pauseAction);
        }

        private void SetupCamera(IObjectResolver obj)
        {
            if (_cameraFollowsPlayer)
            {
                var player = obj.Resolve<KinematicCharacterMotor>();
                _cinemachineCamera.Follow = player.transform;
            }
        }
    }
}