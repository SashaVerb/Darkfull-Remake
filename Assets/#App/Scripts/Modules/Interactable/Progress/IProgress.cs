using System;

namespace Modules.Interactable.Progress
{
    public interface IProgress
    {
        public event Action<float> OnProgressChange;
        public float CurrentProgress { get; }
    }
}