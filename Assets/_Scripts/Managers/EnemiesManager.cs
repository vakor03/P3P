#region

using System;
using _Scripts.Helpers;
using _Scripts.Systems;
using _Scripts.Units.Players;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Scripts.Managers
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        private const float ENEMIES_DEFAULT_Y = 0.6f;

        public void SpawnEnemy(EnemyType enemyType)
        {
            var spawnPoint = GetPointInRadius(Player.Instance.transform.position, 7f, 12f);
            SpawnUnit(enemyType, spawnPoint);
        }

        public void SpawnRandomEnemy()
        {
            EnemyType enemyType = ResourceSystem.Instance.GetRandomEnemy().enemyType;
            
            SpawnEnemy(enemyType);
        }

        private Vector3 GetPointInRadius(Vector3 point, float minRadius, float maxRadius)
        {
            float angle = Random.value * 2 * Mathf.PI;
            float distance = Random.Range(minRadius, maxRadius);

            float x = (int)(point.x + distance * Mathf.Cos(angle));
            float z = (int)(point.z + distance * Mathf.Sin(angle));
            
            return new Vector3(x, ENEMIES_DEFAULT_Y, z);
        }

        private void SpawnUnit(EnemyType enemyType, Vector3 position)
        {
            var enemyScriptable = ResourceSystem.Instance.GetEnemy(enemyType);

            var spawned = Instantiate(enemyScriptable.prefab, position, Quaternion.identity, transform);

            var stats = enemyScriptable.baseStats;
            
            spawned.SetStats(stats);
        }
    }
}