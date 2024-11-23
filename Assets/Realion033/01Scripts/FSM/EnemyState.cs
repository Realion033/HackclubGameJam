using UnityEngine;

namespace Realion033
{
    public enum EnemyStateEnum
    {
        Idle,
        Run,
        Slide
    }

    public abstract class EnemyState
    {
        protected Enemy _enemy;
        protected EnemyStateMachine _machine;
        protected int _animBoolHash;
        private Enemy enemy;
        private EnemyStateMachine machine;

        public EnemyState(Enemy enemy, EnemyStateMachine machine, string boolName)
        {
            _enemy = enemy;
            _machine = machine;
            _animBoolHash = Animator.StringToHash(boolName);
        }

        protected EnemyState(Enemy enemy, EnemyStateMachine machine)
        {
            this.enemy = enemy;
            this.machine = machine;
        }

        public virtual void OnEnter()
        {
            _enemy.animator.SetBool(_animBoolHash, true);
        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnExit()
        {
            _enemy.animator.SetBool(_animBoolHash, false);
        }
    }
}
