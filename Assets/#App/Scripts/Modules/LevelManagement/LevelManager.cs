using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UIManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _App.Scripts.Modules.LevelManagement
{
    public class LevelManager
    {
        public event Action OnLevelReady;

        public int CurrentIndex => _currentIndex;
        public int LastIndex => _lastIndex;

        private readonly LevelConfig _config;
        private readonly UIPanel _transition;

        private int _currentIndex = -1;
        private int _lastIndex = -1;
        private bool _isLoading;

        public LevelManager(LevelConfig config, UIPanel transition)
        {
            _config = config;
            _transition = transition;
        }

        public UniTask LoadNextLevel()
        {
            int nextIndex = _currentIndex + 1;

            if (nextIndex >= _config.Levels.Count)
                nextIndex = 0;

            return LoadLevel(nextIndex);
        }

        public UniTask ResetLevel()
        {
            if (_currentIndex < 0)
            {
                return UniTask.CompletedTask;
            }

            return LoadLevel(_currentIndex);
        }

        public UniTask LoadLastLevel()
        {
            if (_lastIndex < 0)
            {
                return UniTask.CompletedTask;
            }

            return LoadLevel(_lastIndex);
        }

        public async UniTask LoadLevel(int index)
        {
            if (_isLoading)
                return;

            _isLoading = true;

            await _transition.Show();

            _lastIndex = _currentIndex;
            _currentIndex = index;

            await SceneManager.LoadSceneAsync(_config.Levels[index].Name);

            await _transition.Hide();

            _isLoading = false;
            OnLevelReady?.Invoke();
        }
    }
}
