#region

using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using _Scripts.Units.Players;
using MEC;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

#endregion

namespace _Scripts.Units.Enemies.RangedEnemyStates
{
    public class MovingState : IRangedEnemyState
    {
        public void Enter(RangedEnemy context)
        {
            Debug.Log($"Enter {typeof(MovingState)}");
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log($"Exit {typeof(MovingState)}");
        }

        private Vector3 CalculateMovementDirectionNormalized(RangedEnemy context)
        {
            Vector3 playerPosition = Player.Instance.transform.position;
            Vector3 enemyPosition = context.transform.position;

            var playerPositionOnCanvas = new Vector3(playerPosition.x, 0, playerPosition.z);
            var enemyPositionOnCanvas = new Vector3(enemyPosition.x, 0, enemyPosition.z);

            if (context.DistanceToPlayer < context.Stats.minDistanceToPlayer)
            {
                return (enemyPositionOnCanvas - playerPositionOnCanvas).normalized;
            }

            if (context.DistanceToPlayer > context.Stats.attackRange)
            {
                return (playerPositionOnCanvas - enemyPositionOnCanvas).normalized;
            }

            return Vector3.zero;
        }

        public void Update(RangedEnemy context, float deltaTime)
        {
            var movementDirection = CalculateMovementDirectionNormalized(context);
            context.Transform.position +=
                movementDirection * (context.Stats.speed * deltaTime);

            if (movementDirection == Vector3.zero 
                && context.EnemyAttacker.CanAttack())
            {
                context.SwitchState(new AttackingState());
            }
        }
    }

    public class AttackingState : IRangedEnemyState
    {
        private RangedEnemy _context;

        public void Enter(RangedEnemy context)
        {
            Debug.Log("Enter AttackingState");
            _context = context;


            context.EnemyAttacker.PerformAttack();
            context.EnemyAttacker.OnAttackFinished += EnemyAttackerOnAttackFinished;
        }

        private void EnemyAttackerOnAttackFinished()
        {
            _context.SwitchState(new MovingState());
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log("Exit AttackingState");
            context.EnemyAttacker.OnAttackFinished -= EnemyAttackerOnAttackFinished;
        }
    }
}