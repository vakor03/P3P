using _Scripts.HealthSystems;
using _Scripts.Systems;

namespace _Scripts.Units.Enemies
{
    public abstract class EnemyBase : UnitBase
    {}

    public interface IEnemy
    {
        IUnitHealth UnitHealth { get; }
        void SetStats(Stats stats);
        void Init();
    }
}