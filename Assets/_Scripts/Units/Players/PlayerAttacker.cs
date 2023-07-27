#region

using UnityEngine;

#endregion

namespace _Scripts.Units.Players
{
    public class PlayerAttacker : MonoBehaviour
    {
        private const int COLLIDER_BUFFER_COUNT = 10;

        [SerializeField] private SphereCollider attackCollider;
        [SerializeField] private LayerMask enemiesLayerMask;

        private readonly Collider[] _colliderBuffer = new Collider[COLLIDER_BUFFER_COUNT];

        public void PerformAttack()
        {
            Vector3 position = attackCollider.transform.position;
            float radius = attackCollider.radius;
            int count = Physics.OverlapSphereNonAlloc(position, radius, _colliderBuffer, enemiesLayerMask);
            for (int i = 0; i < count; i++)
            {
                if (_colliderBuffer[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage();
                }
            }
        }
    }
}