using _Scripts.Helpers;
using _Scripts.Systems;
using UnityEngine;

namespace _Scripts.Managers
{
    public class DebugKeysSystem : Singleton<DebugKeysSystem>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EnemiesManager.Instance.SpawnEnemy(EnemyType.MeleeEnemy);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EnemiesManager.Instance.SpawnEnemy(EnemyType.RangedEnemy);
            }
        }
    }
}