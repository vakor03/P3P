using _Scripts.Helpers;
using _Scripts.Units;
using _Scripts.Units.Enemies;
using _Scripts.Units.Players;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EffectsManager : StaticInstance<EffectsManager>
    {
        [SerializeField] private Player player;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private GameObject onEnemyDeadParticlesPrefab;
        [SerializeField] private float destroyDelay;


        private void Start()
        {
            player.UnitHealth.OnHealthChanged += PlayerOnHealthChanged;
            MeleeEnemy.OnEnemyDead += EnemyOnEnemyDead;
        }

        private void EnemyOnEnemyDead(Vector3 deadPosition)
        {
            var effectSpawned = Instantiate(onEnemyDeadParticlesPrefab, deadPosition, Quaternion.identity, transform);
            Destroy(effectSpawned, destroyDelay);
        }

        private void PlayerOnHealthChanged(int oldValue, int newValue)
        {
            if (oldValue > newValue)
            {
                impulseSource.GenerateImpulse();
            }
        }

        private void OnDestroy()
        {
            MeleeEnemy.OnEnemyDead -= EnemyOnEnemyDead;
        }
    }
}