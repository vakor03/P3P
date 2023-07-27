using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using _Scripts.Units.Players;
using MEC;
using UnityEngine;

namespace _Scripts.Units.Enemies.RangedEnemyStates
{
    public class MovingTowardsState : IRangedEnemyState
    {
        public void Enter(RangedEnemy context)
        {
            Debug.Log("Enter MovingTowardsState");
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log("Exit MovingTowardsState");
        }

        public void Update(RangedEnemy context)
        {
            context.Transform.position = Vector3.MoveTowards(context.Transform.position,
                Player.Instance.transform.position,
                context.Stats.speed * Time.deltaTime);

            if (context.DistanceToPlayer < context.Stats.minDistanceToPlayer)
            {
                context.SwitchState(new MovingOutwardsState());
            }

            if (context.DistanceToPlayer < context.Stats.attackRange)
            {
                context.SwitchState(new AttackingState());
            }
        }
    }

    public class IdleState : IRangedEnemyState
    {
        public void Enter(RangedEnemy context)
        {
            Debug.Log("Enter IdleState");
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log("Exit IdleState");
        }

        public void Update(RangedEnemy context)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MovingOutwardsState : IRangedEnemyState
    {
        public void Enter(RangedEnemy context)
        {
            Debug.Log("Enter MovingOutwardsState");
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log("Exit MovingOutwardsState");
        }

        public void Update(RangedEnemy context)
        {
            context.Transform.position = Vector3.MoveTowards(context.Transform.position,
                Player.Instance.transform.position,
                -context.Stats.speed * Time.deltaTime);

            if (context.DistanceToPlayer > context.Stats.minDistanceToPlayer &&
                context.DistanceToPlayer < context.Stats.attackRange)
            {
                context.SwitchState(new AttackingState());
            }
        }
    }

    public class AttackingState : IRangedEnemyState
    {
        private float _attackTimer = 5f;
        private bool _isAttacking;

        public void Enter(RangedEnemy context)
        {
            Debug.Log("Enter AttackingState");
        }

        public void Exit(RangedEnemy context)
        {
            Debug.Log("Exit AttackingState");
        }

        public void Update(RangedEnemy context)
        {
            if (_isAttacking)
            {
                return;
            }

            var routineMEC = Coroutines.StartRoutineMEC(AttackRoutine(context));
            Debug.Log("Start AttackRoutine");
        }

        private IEnumerator<float> AttackRoutine(RangedEnemy context)
        {
            _isAttacking = true;
            Debug.Log("State: Start Attacking");

            yield return Timing.WaitForSeconds(_attackTimer);

            Debug.Log("State: Finish Attacking");
            _isAttacking = false;

            if (context.DistanceToPlayer > context.Stats.attackRange)
            {
                context.SwitchState(new MovingTowardsState());
            }
        }
    }
}