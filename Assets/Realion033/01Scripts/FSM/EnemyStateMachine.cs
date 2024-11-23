using UnityEngine;
using System.Collections.Generic;

namespace Realion033
{
    public class EnemyStateMachine
    {
        public Dictionary<EnemyStateEnum, EnemyState> StateDictionary = new();
        public EnemyState _currentState { get; private set; }
        public Enemy _enemy;

        public void Init(EnemyStateEnum startState, Enemy enemy)
        {
            _currentState = StateDictionary[startState];
            _currentState.OnEnter();
            _enemy = enemy;
        }

        public void ChangeState(EnemyStateEnum newState)
        {
            _currentState.OnExit();
            _currentState = StateDictionary[newState];
            _currentState.OnEnter();
        }

        public void MachineUpdate()
        {
            _currentState.OnUpdate();
        }

        public void AddState(EnemyStateEnum stateEnum, EnemyState _enemyState)
        {
            StateDictionary.Add(stateEnum, _enemyState);
        }
    }
}
