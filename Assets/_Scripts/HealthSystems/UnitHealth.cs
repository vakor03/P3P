#region

using System;
using UnityEngine;

#endregion

namespace _Scripts.HealthSystems
{
    public class UnitHealth : IUnitHealth
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public float CurrentHealthNormalized => (float)CurrentHealth / MaxHealth;
        
        public event Action<int,int> OnHealthChanged;
        public event Action OnDead;

        public UnitHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public UnitHealth(int maxHealth, int currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public void ReceiveDamage(int value)
        {
            int oldValue = CurrentHealth;
            Debug.Log(CurrentHealth);
            CurrentHealth = Mathf.Max(0, CurrentHealth - value);
            OnHealthChanged?.Invoke(oldValue, CurrentHealth);

            if (CurrentHealth <= 0)
            {
                OnDead?.Invoke();
            }
        }

        public void Heal(int value)
        {
            int oldValue = CurrentHealth;
            CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + value);
            
            OnHealthChanged?.Invoke(oldValue, CurrentHealth);
        }
    }
}