#region

using System;

#endregion

namespace _Scripts.HealthSystems
{
    public interface IUnitHealth
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        float CurrentHealthNormalized { get; }
        
        event Action<int,int> OnHealthChanged;
        event Action OnDead;

        void ReceiveDamage(int value);
        void Heal(int value);
    }
}