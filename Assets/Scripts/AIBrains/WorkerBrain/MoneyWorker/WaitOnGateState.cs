using Abstract;
using AIBrains.WorkerBrain.MoneyWorker;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class WaitOnGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        public WaitOnGateState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            //Idle anim 
            _navmeshAgent.enabled = false;
        }

        public void OnExit()
        {
            _navmeshAgent.enabled = true;
        }

        public void Tick()
        {
            
        }
    }
}
