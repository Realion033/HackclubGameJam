using System;
using UnityEngine;

namespace Realion033
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy enemy, EnemyStateMachine machine, string boolName) : base(enemy, machine, boolName)
        {
            _enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //SoundManager.Instance.PlayAudio();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            ChasePlayer();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void ChasePlayer()
        {
            Collider target = _enemy.IsPlayerDetected();
            if (target == null) return;

            Vector3 direction = target.transform.position - _enemy.transform.position;
            direction.y = 0;

            if (_enemy.IsObstacleInLine(direction.magnitude, direction.normalized) == false)
            {
                _enemy.targetTrm = target.transform;
                _machine.ChangeState(EnemyStateEnum.Run);
            }
        }
    }
}
