using System;
using UIManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Features.Pause
{
    public class PauseView : UIPanel, IInitializable, IDisposable
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        
        private PausePresenter _presenter;

        [Inject]
        private void Configure(PausePresenter presenter)
        {
            _presenter = presenter;
        }
        
        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(_presenter.Resume);
            _quitButton.onClick.AddListener(_presenter.Quit);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        public void Initialize()
        {
            _presenter.OnResume += Hide;
            _presenter.OnPause += Show;

            if (!_presenter.IsPaused)
            {
                HideImmediate();
            }
        }

        public void Dispose()
        {
            _presenter.OnResume -= Hide;
            _presenter.OnPause -= Show;
            
            Destroy(gameObject);
        }
    }
}
