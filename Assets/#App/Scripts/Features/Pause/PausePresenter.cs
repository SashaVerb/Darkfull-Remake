using System;
using System.Collections.Generic;
using _App.Scripts.Modules.LevelManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _App.Scripts.Features.Pause
{
    public class PausePresenter : IInitializable, IDisposable
    {
        public event Action OnResume;
        public event Action OnPause;

        public bool IsPaused { get; private set; }
        
        private readonly InputActionReference _pauseAction;
        private readonly IReadOnlyList<IPausable> _pausableBehaviours;
        private readonly LevelManager _levelManager;
        
        public PausePresenter(IReadOnlyList<IPausable> pausableBehaviours, LevelManager levelManager, InputActionReference pauseAction)
        {
            _pausableBehaviours = pausableBehaviours;
            _levelManager = levelManager;
            _pauseAction = pauseAction;
        }

        public void Initialize()
        {
            _pauseAction.action.started += TogglePause;
        }

        public void Resume()
        {
            if(!IsPaused)
                return;
            
            Time.timeScale = 1f;
            IsPaused = false;
            
            foreach (var pausableBehaviour in _pausableBehaviours)
            {
                pausableBehaviour.Resume();
            }
            
            OnResume?.Invoke();
        }

        public void Pause()
        {
            if(IsPaused)
                return;

            Time.timeScale = 0f;
            IsPaused = true;
            
            foreach (var pausableBehaviour in _pausableBehaviours)
            {
                pausableBehaviour.Pause();
            }
            
            OnPause?.Invoke();
        }

        public void Quit()
        {
            _levelManager.LoadScene("Menu");
        }

        private void TogglePause(InputAction.CallbackContext _)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Dispose()
        {
            _pauseAction.action.started -= TogglePause;
        }
    }
}
