using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class MoveToBomb : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        
        public MoveToBomb(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}