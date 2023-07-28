#region

using _Scripts.HealthSystems;
using _Scripts.Units.Enemies.RangedEnemyStates;
using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies
{
    public class RangedEnemy : EnemyBase
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
            UnitHealth = new UnitHealth(Stats.health);
            SetDefaultState();
            EnemyAttacker = GetComponent<EnemyAttacker>();
        }

        private void Update()
        {
            _currentState.Update(this, Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPlayer player))
            {
                player.UnitHealth.ReceiveDamage(Stats.damage);
            }
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
    }
}