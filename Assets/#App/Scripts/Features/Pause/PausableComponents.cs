using UnityEngine;

namespace _App.Scripts.Features.Pause
{
    public class PausableComponents : IPausable
    {
        private readonly MonoBehaviour[] _components;
        
        public PausableComponents(MonoBehaviour[] components)
        {
            _components = components;
        }
        
        public void Pause()
        {
            foreach (var component in _components)
            {
                component.enabled = false;
            }
        }

        public void Resume()
        {
            foreach (var component in _components)
            {
                component.enabled = true;
            }
        }
    }
}