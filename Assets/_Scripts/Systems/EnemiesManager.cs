#region

using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts.Systems
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        public void SpawnEnemy()
        {
            var spawnPoint = GetPointInRadius(Player.Instance.transform.position, 5f, 7f);
            SpawnUnit(EnemyType.MeleeEnemy, spawnPoint);
        }

        private Vector3 GetPointInRadius(Vector3 point, float minRadius, float maxRadius)
        {
            float angle = Random.value * 2 * Mathf.PI;
            float distance = Random.Range(minRadius, maxRadius);

            float x = (int)(point.x + distance * Mathf.Cos(angle));
            float z = (int)(point.z + distance * Mathf.Sin(angle));

            return new Vector3(x, point.y, z);
        }

        private void SpawnUnit(EnemyType enemyType, Vector3 position)
        {
            var enemyScriptable = ResourceSystem.Instance.GetEnemy(enemyType);

            var spawned = Instantiate(enemyScriptable.prefab, position, Quaternion.identity);

            var stats = enemyScriptable.baseStats;
            
            spawned.SetStats(stats);
        }
    }
}