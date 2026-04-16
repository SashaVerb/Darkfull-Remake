using UnityEngine;

namespace _App.Scripts.Modules.Interactable
{
    [RequireComponent(typeof(Animator))]
    public class InteractableAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int ActiveHash = Animator.StringToHash("Active");
        
        public void Activate()
        {
            _animator.SetBool(ActiveHash, true);
        }
        
        public void Deactivate()
        {
            _animator.SetBool(ActiveHash, false);
        }

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }
    }
}