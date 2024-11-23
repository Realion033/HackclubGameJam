namespace Realion033
{
    public enum EnemyStateEnum
    {
        Idle
    }

    public abstract class EnemyState
    {
        protected Enemy _enemy;
        protected EnemyStateMachine _machine;

        public EnemyState(Enemy enemy, EnemyStateMachine machine)
        {
            _enemy = enemy;
            _machine = machine;
        }

        public virtual void OnEnter()
        {
            
        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnExit()
        {

        }
    }
}
