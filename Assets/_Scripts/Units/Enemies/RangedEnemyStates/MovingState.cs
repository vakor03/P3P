#region

using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies.RangedEnemyStates
{
    public class MovingState : IRangedEnemyState
    {
        private Vector3 _finalPosition;

        public void Enter(RangedEnemy context)
        {
            var finalDistance = (context.Stats.minDistanceToPlayer + context.Stats.attackRange) / 2;
            _finalPosition = ChooseFinalPosition(context, finalDistance);
        }

        private Vector3 ChooseFinalPosition(RangedEnemy context, float finalDistance)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 enemyPosition = context.transform.position;

            var playerPositionOnCanvas = new Vector3(playerPosition.x, 0, playerPosition.z);
            var enemyPositionOnCanvas = new Vector3(enemyPosition.x, 0, enemyPosition.z);

            return playerPosition +
                   (enemyPositionOnCanvas - playerPositionOnCanvas).normalized * finalDistance;
        }

        public void Update(RangedEnemy context, float deltaTime)
        {
            if (_finalPosition == context.Transform.position
                && context.EnemyAttacker.CanAttack())
            {
                context.SwitchState(new AttackingState());
            }

            context.Transform.position =
                Vector3.MoveTowards(context.Transform.position,
                    _finalPosition,
                    context.Stats.speed * deltaTime);
        }
    }
}