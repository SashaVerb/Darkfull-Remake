using System;
using UnityEngine;

namespace _App.Scripts.Features.LevelLogic
{
    public class LevelComplete : MonoBehaviour
    {
        public event Action OnLevelComplete;

        public void CompleteLevel()
        {
            OnLevelComplete?.Invoke();
        }
    }
}
