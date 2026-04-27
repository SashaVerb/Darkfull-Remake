using System.Collections.Generic;
using UnityEngine;

namespace _App.Scripts.Modules.LevelManagement
{
    [CreateAssetMenu(menuName = "App/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private SceneReference[] _levels;

        public IReadOnlyList<SceneReference> Levels => _levels;
    }
}
