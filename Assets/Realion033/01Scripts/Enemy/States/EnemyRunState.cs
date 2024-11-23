using UnityEngine;

namespace Realion033
{
    public class EnemyRunState : EnemyState
    {
        private Vector3 _dashDirection; // 대시 방향 저장용

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

            // 기존의 움직임과 타겟 처리
            Debug.Log("Update");
            _enemy.movement.SetDestination(_enemy.targetTrm.position);
            _enemy.movement.LookToTarget(_enemy.targetTrm.position);

            ChasePlayerAttack();
            ChasePlayer();
        }

        private void ChasePlayerAttack()
        {
            Collider target = _enemy.IsPlayerOnAttackDetected();
            if (target == null)
            {
                Debug.LogWarning("No player detected for attack.");
                return;
            }

            if (_enemy.targetTrm == null)
            {
                Debug.LogError("Enemy's targetTrm is null. Aborting attack.");
                return;
            }

            // 대시 방향 설정
            Vector3 direction = (_enemy.targetTrm.position - _enemy.transform.position).normalized;
            _dashDirection = new Vector3(direction.x, 0, direction.z).normalized;

            if (!_enemy.IsObstacleInLine(direction.magnitude, direction))
            {
                _machine.ChangeState(EnemyStateEnum.Slide);
            }
        }


        private void ChasePlayer()
        {
            Collider target = _enemy.IsPlayerDetected();
            if (target != null)
            {
                return;
            }

            if (target == null)
            {
                _enemy.movement.StopImmediately();
                _machine.ChangeState(EnemyStateEnum.Idle);
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            // 대시 방향 정보 초기화
            _dashDirection = Vector3.zero;
        }
    }
}
