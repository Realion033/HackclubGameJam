using UnityEngine;

namespace Realion033
{
    public class EnemyRunState : EnemyState
    {
        public EnemyRunState(Enemy enemy, EnemyStateMachine machine, string boolName) : base(enemy, machine, boolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _enemy.movement.SetDestination(_enemy.targetTrm.position);
            _enemy.movement.LookToTarget(_enemy.targetTrm.position);
        }
    }
}
