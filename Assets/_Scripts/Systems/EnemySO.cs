#region

using _Scripts.Units;
using UnityEngine;

#endregion

namespace _Scripts.Systems
{
    [CreateAssetMenu(menuName = "Create EnemySO", fileName = "EnemySO", order = 0)]
    public class EnemySO : ScriptableObject
    {
        public EnemyType enemyType;
        public EnemyBase prefab;
        public Stats baseStats;
    }
}