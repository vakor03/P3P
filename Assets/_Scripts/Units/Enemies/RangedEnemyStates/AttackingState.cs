namespace _Scripts.Units.Enemies.RangedEnemyStates
{
    public class AttackingState : IRangedEnemyState
    {
        private RangedEnemy _context;
        private bool _needsMovement;

        public void Enter(RangedEnemy context)
        {
            _context = context;

            if (context.DistanceToPlayer > context.Stats.attackRange)
            {
                _needsMovement = true;
                return;
            }
            
            context.EnemyAttacker.PerformAttack();
            context.EnemyAttacker.OnAttackFinished += EnemyAttackerOnAttackFinished;
        }

        private void EnemyAttackerOnAttackFinished()
        {
            _context.SwitchState(new MovingState());
        }

        public void Update(RangedEnemy context, float deltaTime)
        {
            if (_needsMovement)
            {
                context.SwitchState(new MovingState());
            }
        }

        public void Exit(RangedEnemy context)
        {
            if (_needsMovement)
            {
                _needsMovement = false;
                return;
            }
            
            context.EnemyAttacker.StopAttack();
            context.EnemyAttacker.OnAttackFinished -= EnemyAttackerOnAttackFinished;
        }
    }
}