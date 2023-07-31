#region

using _Scripts.HealthSystems;

#endregion

namespace _Scripts.Units.Players
{
    public interface IPlayer
    {
        DamageReceiver DamageReceiver { get; }
    }
}