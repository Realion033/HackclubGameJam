using UnityEngine;
using UnityEngine.AI;
using System;

namespace Realion033
{
    public class Enemy : MonoBehaviour
    {
        [Header("EnemySetting")]
        public float speed;
        [SerializeField] private LayerMask _whatIsPlayer;

        public NavMeshAgent navMesh { get; private set; }
        public Rigidbody rb { get; private set; }
        public Animator animator { get; private set; }
        private EnemyStateMachine _machine = new EnemyStateMachine();

        #region UNITY_EVENTS

        private void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();

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
                    Debug.Log(t);
                    EnemyState state = Activator.CreateInstance(t, this, _machine) as EnemyState;
                    Debug.Log(state);

                    _machine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{typeName} is loading error!");
                    Debug.LogError(ex);
                }
            }
        }

        #endregion
    }
}