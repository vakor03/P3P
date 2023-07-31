#region

using System;

#endregion

namespace _Scripts
{
    public interface IDamageable
    {
        event Action OnDamageTaken;
        void ReceiveDamage();
    }
}