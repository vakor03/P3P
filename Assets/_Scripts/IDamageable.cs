using System;

namespace _Scripts
{
    public interface IDamageable
    {
        event Action OnDamageTaken;
        void TakeDamage();
    }
}