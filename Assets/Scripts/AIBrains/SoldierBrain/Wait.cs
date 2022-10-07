using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class Wait : IState
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public Wait(Animator animator,NavMeshAgent navMeshAgent)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            Debug.Log("WaitEntered");
            _navMeshAgent.speed = 1.801268E-05f;
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
        }
        public void OnExit()
        {
            
        }
    }
}