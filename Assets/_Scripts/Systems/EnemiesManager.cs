using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Systems
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        public void SpawnEnemy()
        {
            SpawnUnit(EnemyType.MeleeEnemy, Player.Instance.transform.position + Vector3.forward);
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