using UnityEngine;
using System;

namespace Realion033
{
    public class Enemy : MonoBehaviour
    {
        [Header("EnemySetting")]
        public float speed;
        public float runAwayDistance;
        public float onAttackDistance;
        [SerializeField] public Transform targetTrm;
        [SerializeField] public Transform endTrm;
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private LayerMask _whatIsObstacle;

        [Header("Sound Source")]
        public AudioClip chaseSound;
        public AudioClip slideSound;
        public AudioClip die;

        //component
        public Animator animator;
        public EnemyMovement movement;
        public AudioSource _audioSource;
        private EnemyStateMachine _machine = new EnemyStateMachine();

        //protected
        protected Collider[] _enemyCheckColliders;
        protected Collider[] _enemyCheckCollidersAttack;

        #region UNITY_EVENTS

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            movement = GetComponent<EnemyMovement>();
            _audioSource = GetComponent<AudioSource>();

            //endTrm = GameObject.Find("SetDestiend").GetComponent<Transform>();

            _enemyCheckColliders = new Collider[99];
            _enemyCheckCollidersAttack = new Collider[99];
            
            ReflectionStates();
        }

        private void Start()
        {
            _machine.Init(EnemyStateEnum.Idle, this);
        }

        private void Update()
        {
            _machine.MachineUpdate();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, runAwayDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, onAttackDistance);
        }

        #endregion

        #region MAIN_FUNC

        public Collider IsPlayerDetected()
        {
            int cnt = Physics.OverlapSphereNonAlloc(transform.position, runAwayDistance, 
            _enemyCheckColliders, _whatIsPlayer);
            Debug.Log(cnt);
            return cnt >= 1 ? _enemyCheckColliders[0] : null;
        }

        public Collider IsPlayerOnAttackDetected()
        {
            int cnt = Physics.OverlapSphereNonAlloc(transform.position, onAttackDistance, 
            _enemyCheckCollidersAttack, _whatIsPlayer);

            return cnt >= 1 ? _enemyCheckColliders[0] : null;
        }

        public virtual bool IsObstacleInLine(float distance, Vector3 direction)
        {
            return Physics.Raycast(transform.position, direction, distance, _whatIsObstacle);
        }
        #endregion

        #region SUB_FUNC

        private void ReflectionStates()
        {
            foreach (EnemyStateEnum stateEnum in Enum.GetValues(typeof(EnemyStateEnum)))
            {
                string typeName = stateEnum.ToString();
                try
                {
                    Type t = Type.GetType($"Realion033.Enemy{typeName}State");
                    //Debug.Log(t);
                    EnemyState state = Activator.CreateInstance(t, this, _machine, typeName) as EnemyState;
                    //Debug.Log(state);

                    _machine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    //Debug.LogError($"{typeName} is loading error!");
                    Debug.LogError(ex);
                }
            }

            _machine.Init(EnemyStateEnum.Idle, this);
        }

        #endregion
    }
}