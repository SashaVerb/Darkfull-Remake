using UnityEngine.Events;

namespace Modules.Interactable
{
    public interface IInteractable
    {
        bool IsActive { get; }

        UnityEvent OnActivate { get; }
        UnityEvent OnDeactivate { get; }
    
        void Activate();
        void Deactivate();
    }
}
