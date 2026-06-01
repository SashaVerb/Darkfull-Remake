using UnityEngine;
using UnityEngine.Events;

namespace Modules.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool IsActive { get; protected set; }

        [field:SerializeField] public UnityEvent OnActivate { get; private set; }
        [field:SerializeField] public UnityEvent OnDeactivate { get; private set; }
    
        public abstract void Activate();
        public abstract void Deactivate();

        private void Awake()
        {
            if(IsActive)
                Activate();
            else
                Deactivate();
        }
    }
}
