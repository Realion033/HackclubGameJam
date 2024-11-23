using UnityEngine;

namespace Realion033
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy enemy, EnemyStateMachine machine) : base(enemy, machine)
        {   
            _enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log(_enemy.speed);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.Log($"승승병신{_enemy.speed}");
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
