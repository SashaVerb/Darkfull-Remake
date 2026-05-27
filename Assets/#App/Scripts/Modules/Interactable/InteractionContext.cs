using UnityEngine;
using VContainer;

namespace _App.Scripts.Modules.Interactable
{
    public class InteractionContext : MonoBehaviour
    {
        public IObjectResolver ObjectResolver { get; private set; }
        
        public void Provide(IObjectResolver resolver)
        {
            ObjectResolver = resolver;    
        }

        public void Revoke()
        {
            ObjectResolver = null;    
        }
    }
}