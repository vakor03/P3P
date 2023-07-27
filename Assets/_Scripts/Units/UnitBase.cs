#region

using _Scripts.Systems;
using UnityEngine;

#endregion

namespace _Scripts.Units
{
    public abstract class UnitBase : MonoBehaviour
    {
        public Stats Stats;

        public void SetStats(Stats stats)
        {
            Stats = stats;
        }
    }
}