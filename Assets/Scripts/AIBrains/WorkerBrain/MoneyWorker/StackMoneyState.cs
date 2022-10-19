using UnityEngine;
using UnityEngine.AI;
using System;
using Abstract;
using AIBrains.WorkerBrain.MoneyWorker;


namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class StackMoneyState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private bool isArrive;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private readonly float _speed;

        public Func<bool> IsArriveToMoney() => () => isArrive && _moneyWorkerAIBrain.IsAvailable();

        public StackMoneyState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain,float speed)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
            _speed = speed;
        }
        public void OnEnter()
        {
            _navmeshAgent.speed = _speed;
        }

        public void OnExit()
        {
            isArrive = false;
        }
        public void Tick()
        {
            if (_navmeshAgent.remainingDistance <= 0.1f)
            {
                _moneyWorkerAIBrain.CurrentTarget = null;
                isArrive = true;
            }
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
        }
    }
}
