using System.Collections.Generic;
using UnityEngine;

namespace Features.PlayerLogic
{
    public class PlayerSpawn
    {
        private readonly List<Transform> _spawnPoints;
        private float _lastDeathX = float.MinValue;

        public PlayerSpawn(List<Transform> spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        public void ReportDeath(Vector3 playerPosition)
        {
            if (playerPosition.x > _lastDeathX)
                _lastDeathX = playerPosition.x;
        }

        public Vector3 GetSpawnPosition()
        {
            Transform best = _spawnPoints[0];

            foreach (Transform point in _spawnPoints)
            {
                if (_lastDeathX >= point.position.x && point.position.x >= best.position.x)
                    best = point;
            }

            return best.position;
        }
    }
}
