using UnityEngine;
using UnityEngine.Events;

namespace Modules.Interactable.Progress
{
    public abstract class Progress : MonoBehaviour
    {
        [field:SerializeField] public UnityEvent<float> OnProgressChange { get; private set; }
        [field:SerializeField] public UnityEvent<float> OnProgressMax { get; private set; }
        [field:SerializeField] public UnityEvent<float> OnProgressMin { get; private set; }

        public float CurrentProgress { get; protected set; }
    }
}