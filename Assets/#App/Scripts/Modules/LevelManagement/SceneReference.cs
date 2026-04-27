using System;
using UnityEngine;

namespace _App.Scripts.Modules.LevelManagement
{
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
        private string _sceneName;

        public string Name => _sceneName;

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            _sceneName = _sceneAsset != null ? _sceneAsset.name : string.Empty;
#endif
        }

        public void OnAfterDeserialize() { }
    }
}
