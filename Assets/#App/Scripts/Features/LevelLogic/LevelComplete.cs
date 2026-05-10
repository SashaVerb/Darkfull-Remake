using System;
using Modules.Interactable;
using UnityEngine;
using VContainer;

namespace _App.Scripts.Features.LevelLogic
{
    public class LevelComplete : MonoBehaviour, IInteractionContext
    {
        public event Action OnLevelComplete;

        private IInteractable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.OnActivate.AddListener(CompleteLevel);
        }

        private void OnDisable()
        {
            _interactable.OnActivate.RemoveListener(CompleteLevel);
        }

        public void Provide(IObjectResolver resolver)
        {
        }

        public void Revoke()
        {
        }

        private void CompleteLevel()
        {
            OnLevelComplete?.Invoke();
        }
    }
}
