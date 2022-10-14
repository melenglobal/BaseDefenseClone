using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class MoveToWaitZone : IState
    {
        private NavMeshAgent _navMeshAgent;
        private bool _hasReachToTarget;
        private Vector3 _soldierPosition;
        private Vector3 _slotPosition;
        private float _stoppingDistance;
        private SoldierAIBrain _soldierAIBrain;
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public MoveToWaitZone(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent,bool hasReachToTarget,Vector3 slotPosition,Animator animator)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
            _hasReachToTarget = hasReachToTarget;
            _slotPosition = slotPosition;
            _stoppingDistance = navMeshAgent.stoppingDistance;
            _animator = animator;
        } 
        public void Tick()
        {
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            if (!((_navMeshAgent.transform.position - _slotPosition).sqrMagnitude < _stoppingDistance)) return;
            _hasReachToTarget = true;
            _soldierAIBrain.HasReachedSlotTarget = _hasReachToTarget;
        } 
        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_slotPosition);
            _navMeshAgent.speed = 1.80f; //Data
        }
        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
    }
}