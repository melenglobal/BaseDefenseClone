using Abstract;
using AIBrains.WorkerBrain.MoneyWorker;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class SearchState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public SearchState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            _navmeshAgent.speed = 0f;
            if (_moneyWorkerAIBrain.IsAvailable())
            {
                _moneyWorkerAIBrain.StartSearch(true);
            }
        }

        public void OnExit()
        {
            _moneyWorkerAIBrain.StartSearch(false);
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
        }


    } 
}
