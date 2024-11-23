using UnityEngine;
using UnityEngine.AI;

namespace Realion033
{
    public class EnemyMovement : MonoBehaviour
    {
        private Enemy _enemy;
        private NavMeshAgent _navMesh;
        private Rigidbody _rb;

        #region UNITY_FUNC

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _navMesh = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

            _navMesh.speed = _enemy.speed;
        }

        #endregion
        #region MainFunc

        public void SetDestination(Vector3 destination)
        {
            if (_navMesh.enabled == false) return;

            _navMesh.isStopped = false;
            _navMesh.SetDestination(destination);
        }

        public void StopImmediately()
        {
            if (_navMesh.enabled == false) return;

            _navMesh.isStopped = true;
        }

        public void LookToTarget(Vector3 targetPos)
        {
            float rotationSpeed = 250f; // 값을 더 크게 설정
            Vector3 toward = targetPos - transform.position;
            toward.y = 0; // y 축 회전은 고려하지 않음

            // 현재 회전 값과 목표 회전 값을 구함
            Quaternion targetRotation = Quaternion.LookRotation(toward);

            // 회전 속도에 맞게 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }



        #endregion
    }
}
