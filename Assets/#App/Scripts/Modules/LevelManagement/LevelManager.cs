using System;
using System.Threading;
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
        public bool IsLoading => _isLoading;

        private readonly LevelConfig _config;
        private readonly UIPanel _transition;

        private int _currentIndex;
        private int _lastIndex = -1;
        private bool _isLoading;
        private CancellationTokenSource _loadCts;

        public LevelManager(LevelConfig config, UIPanel transition)
        {
            _config = config;
            _transition = transition;
            
            _currentIndex = SceneManager.GetActiveScene().buildIndex;
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
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _loadCts = new CancellationTokenSource();
            var token = _loadCts.Token;

            _isLoading = true;

            try
            {
                await _transition.Show().AttachExternalCancellation(token);

                _lastIndex = _currentIndex;
                _currentIndex = index;

                await SceneManager.LoadSceneAsync(_config.Levels[index].Name).ToUniTask(cancellationToken: token);

                await _transition.Hide().AttachExternalCancellation(token);

                _isLoading = false;
                OnLevelReady?.Invoke();
            }
            catch (OperationCanceledException)
            {
                _isLoading = false;
            }
        }
    }
}
