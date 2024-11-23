using UnityEngine;
using UnityEngine.AI;

namespace Realion033{
    public class AnimationTrigger : MonoBehaviour
    {
        private NavMeshAgent _navMesh;
        private Animator _animator;
        private Rigidbody _rb;
        private Enemy _enemy;

        private void Awake()
        {
            _navMesh = GetComponentInParent<NavMeshAgent>();
            _animator = GetComponentInParent<Animator>();
            _rb = GetComponentInChildren<Rigidbody>();
            _enemy = GetComponentInParent<Enemy>();
        }

            public void DestinationOff()
            {
                _rb.AddForce(transform.forward * _enemy.speed * 5f, ForceMode.Impulse);
                _navMesh.enabled = false;
                _animator.enabled = false;
            }
    }
}
