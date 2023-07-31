using System;
using _Scripts.HealthSystems;

namespace _Scripts.Units.Enemies
{
    public class SingleHpUnitHealth : IUnitHealth
    {
        public int MaxHealth => 1;
        public int CurrentHealth { get; private set; }
        public float CurrentHealthNormalized => CurrentHealth;
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;

        public void ReceiveDamage(int value)
        {
            if (value <= 0)
            {
                return;
            }

            CurrentHealth = 0;
            OnHealthChanged?.Invoke(1, 0);
            OnDead?.Invoke();
        }

        public void Heal(int value)
        {
        }
    }
}