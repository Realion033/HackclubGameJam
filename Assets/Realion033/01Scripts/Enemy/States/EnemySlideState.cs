using UnityEngine;

namespace Realion033
{
    public class EnemySlideState : EnemyState
    {
        private Vector3 _dashDirection; // 슬라이드 방향
        private float _dashDuration = 1f; // 슬라이드 지속 시간
        private float _elapsedTime = 0f; // 슬라이드 경과 시간

        public EnemySlideState(Enemy enemy, EnemyStateMachine machine, string boolName) : base(enemy, machine, boolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemy._audioSource.clip = _enemy.slideSound;
            _enemy._audioSource.loop = false;
            _enemy._audioSource.Play();
            // 슬라이드 방향 계산 (현재 적의 진행 방향 고정)
            Vector3 direction = _enemy.targetTrm.position - _enemy.transform.position;
            _dashDirection = direction.normalized;

            // 속도 증가
            _enemy.speed *= 3f;

            // 초기화
            _elapsedTime = 0f;

            Debug.Log("Enemy started sliding!");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // 슬라이드 이동 처리
            _elapsedTime += Time.deltaTime;
            _enemy.transform.position += _dashDirection * _enemy.speed * Time.deltaTime;

            // 슬라이드 종료 조건 (지속 시간 초과)
        }

        public override void OnExit()
        {
            base.OnExit();

            // // 속도 복구
            // _enemy.speed /= 3f;

            // Debug.Log("Enemy slide state exited.");
        }
    }
}
