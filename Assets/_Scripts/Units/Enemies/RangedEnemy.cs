#region

using System;
using _Scripts.Units.Enemies.RangedEnemyStates;
using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public class RangedEnemy : EnemyBase, IDamageable
    {
        private IRangedEnemyState _currentState;
        public float DistanceToPlayer => CalculateDistanceToPlayer();
        public Transform Transform => transform;
        public EnemyAttacker EnemyAttacker { get; private set; }

        private float CalculateDistanceToPlayer()
        {
            var playerPosition = Player.Instance.transform.position;
            return Vector3.Distance(
                new Vector3(playerPosition.x, 0, playerPosition.z),
                new Vector3(Transform.position.x, 0, Transform.position.z));
        }

        private void Awake()
        {
            UnitHealth = new SingleHpUnitHealth();
            
            EnemyAttacker = GetComponent<EnemyAttacker>();
        }

        private void Start()
        {
            UnitHealth.OnDead += () =>
            {
                HandleDeath(transform.position);
                Destroy(gameObject);
            };
            SetDefaultState();
        }

        private void Update()
        {
            _currentState.Update(this, Time.deltaTime);
        }

        public void SwitchState(IRangedEnemyState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }

            _currentState = newState;
            _currentState.Enter(this);
        }

        private void SetDefaultState()
        {
            SwitchState(new MovingState());
        }

        public event Action OnDamageTaken;
        public void ReceiveDamage()
        {
            UnitHealth.ReceiveDamage(1);
        }

        // public void TakeDamage()
        // {
        //     throw new NotImplementedException();
        // }
    }
}