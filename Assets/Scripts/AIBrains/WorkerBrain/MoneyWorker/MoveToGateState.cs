using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.WorkerBrain.MoneyWorker
{
    public class MoveToGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _initPosition;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private readonly float _speed;

        public bool IsArrive = false;
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator, Vector3 initPosition,float speed)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _initPosition = initPosition;
            _speed = speed;

        }
        public void OnEnter()
        {
            _navmeshAgent.speed = _speed;
            _navmeshAgent.SetDestination(_initPosition);
        }

        public void OnExit()
        {
            IsArrive = false;
            _navmeshAgent.transform.rotation = Quaternion.Slerp(_navmeshAgent.transform.rotation, new Quaternion(0, 0, 0, 0),0.5f);
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
            if (_navmeshAgent.remainingDistance < 0.1f)
            {
                IsArrive=true;
            }
        }
    } 
}
