using System;
using _Scripts.Helpers;
using _Scripts.Units;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

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
            player.OnDamageTaken += PlayerOnDamageTaken;
            MeleeEnemy.OnEnemyDead += EnemyOnEnemyDead;
        }

        private void EnemyOnEnemyDead(Vector3 deadPosition)
        {
            var effectSpawned = Instantiate(onEnemyDeadParticlesPrefab, deadPosition, Quaternion.identity, transform);
            Destroy(effectSpawned, destroyDelay);
        }

        private void PlayerOnDamageTaken()
        {
            impulseSource.GenerateImpulse();
        }
    }
}