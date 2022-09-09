using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Chase : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        public bool isPlayerInRange; // check distance agent.remaining
        public Chase(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            //artik kos,
        }

        public void OnExit()
        {
            //kosma
        }
    }
}