using UnityEngine;
using UnityEngine.AI;
using System;

namespace Realion033
{
    public class Enemy : MonoBehaviour
    {
        [Header("EnemySetting")]
        public float speed { get; set; }
        [SerializeField] private LayerMask _whatIsPlayer;

        public NavMeshAgent _navMesh { get; private set; }
        public Rigidbody _rb { get; private set; }
        private EnemyStateMachine _machine = new EnemyStateMachine();

        #region UNITY_EVENTS

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

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
                    Type t = Type.GetType($"Enemy{typeName}State");
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