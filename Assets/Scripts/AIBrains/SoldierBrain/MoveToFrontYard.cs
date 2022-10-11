using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class MoveToFrontYard: IState
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _frontYardSoldierPosition;
        private float _stoppingDistance;
        private SoldierAIBrain _soldierAIBrain;
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attacked = Animator.StringToHash("Attack");

        public MoveToFrontYard(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,Transform frontYardSoldierPosition,Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _frontYardSoldierPosition = frontYardSoldierPosition;
            _stoppingDistance = navMeshAgent.stoppingDistance;
            _soldierAIBrain = soldierAIBrain;
            _animator = animator;
        }
        public void Tick()
        {
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            if ((_navMeshAgent.transform.position - _frontYardSoldierPosition.position).sqrMagnitude < _stoppingDistance)
            {
                _soldierAIBrain.HasReachedFrontYard = true;
            }
        } 
        public void OnEnter()
        {
            _animator.SetTrigger(Attacked);
            _navMeshAgent.speed = 1.5f;
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_frontYardSoldierPosition.position);
            _navMeshAgent.speed = 5.273528f;
        }
        public void OnExit()
        {
            _animator.ResetTrigger(Attacked);
        }
    }
}