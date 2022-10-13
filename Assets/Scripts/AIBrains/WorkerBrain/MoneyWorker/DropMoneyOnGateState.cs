using Abstract;
using UnityEngine;
using UnityEngine.AI;


namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class DropMoneyOnGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _startPos;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public DropMoneyOnGateState(NavMeshAgent navMeshAgent, Animator animator, Vector3 startPos)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _startPos = startPos;
        }
        public void OnEnter()
        {
            _navmeshAgent.SetDestination(_startPos);

        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
        }
    }
}
