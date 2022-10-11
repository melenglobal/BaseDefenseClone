using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class MoveToGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _initPosition;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public bool IsArrive = false;
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator,ref Vector3 initPosition)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _initPosition = initPosition;
        }
        public void OnEnter()
        {
            //isWalking anim
            _navmeshAgent.SetDestination(_initPosition);
            _navmeshAgent.speed = 1.53f;
        }

        public void OnExit()
        {
            IsArrive = false;
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
            if (_navmeshAgent.remainingDistance <= 0.1f)
            {
                IsArrive=true;
            }
        }
    } 
}
