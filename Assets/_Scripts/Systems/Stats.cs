#region

using System;

#endregion

namespace _Scripts.Systems
{
    [Serializable]
    public struct Stats
    {
        public float speed;
        public int health;
        public int damage;
        public float attackSpeed;
        public float attackRange;
        public float minDistanceToPlayer;
    }
}