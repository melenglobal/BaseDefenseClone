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
        private Vector3 frontYardRandomPos;

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
            if ((_navMeshAgent.transform.position - _frontYardSoldierPosition.position-frontYardRandomPos).sqrMagnitude < _stoppingDistance)
            {
                _soldierAIBrain.HasReachedFrontYard = true;
            }
        } 
        public void OnEnter()
        {
            frontYardRandomPos = new Vector3(Random.Range(-20,20), 0, Random.Range(15, 25)); // DAta
            _animator.SetTrigger(Attacked);
            _navMeshAgent.speed = 1.5f; //Data
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_frontYardSoldierPosition.position+frontYardRandomPos);
            _navMeshAgent.speed = 5.273528f; // Data
        }
        public void OnExit()
        {
            _animator.ResetTrigger(Attacked);
        }
    }
}