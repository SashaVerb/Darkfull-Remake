using UnityEngine;
using UnityEngine.Events;

public class EnableStateEvents : MonoBehaviour
{
    public UnityEvent onEnable;
    public UnityEvent onDisable;
    
    public void Enable()
    {
        Debug.Log("Enable");
        onEnable.Invoke();
    }
    
    public void Disable()
    {
        Debug.Log("Disable");
        onDisable.Invoke();
    }
}
