using System.Collections.Generic;
using UnityEngine;

namespace _App.Scripts.Modules.LevelManagement
{
    [CreateAssetMenu(menuName = "App/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private SceneReference[] _levels;

        [SerializeField] private SceneReference[] _scenes;

        public IReadOnlyList<SceneReference> Levels => _levels;

        public bool ContainsScene(string name)
        {
            foreach (var scene in _scenes)
            {
                if (scene.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
