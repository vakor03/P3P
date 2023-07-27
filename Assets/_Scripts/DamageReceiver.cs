using System;
using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts
{
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private float invincibleAfterDamageTime;
        private bool _isInvincibleAfterDamage;
        
        public event Action<int> OnDamageReceived;
        public bool IsPlayer => true;
        public bool IsInvincible { get; set; }

        public void ReceiveDamage(int damage)
        {
            if (_isInvincibleAfterDamage || IsInvincible)
            {
                return;
            }
            
            OnDamageReceived?.Invoke(damage);
            StartCoroutine(InvincibleRoutine());
        }

        private IEnumerator InvincibleRoutine()
        {
            _isInvincibleAfterDamage = true;
            yield return Helper.GetCachedWaitForSeconds(invincibleAfterDamageTime);
            _isInvincibleAfterDamage = false;
        }
    }
}