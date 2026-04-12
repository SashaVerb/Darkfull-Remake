using UnityEngine.Events;

public interface IIndicator
{
    UnityEvent OnActivate { get; }
    UnityEvent OnDeactivate { get; }
    
    void Activate();
    void Deactivate();
}
