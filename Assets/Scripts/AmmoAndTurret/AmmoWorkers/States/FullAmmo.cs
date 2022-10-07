using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class FullAmmo :IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;

        public FullAmmo(NavMeshAgent agent, Animator animator, float movementSpeed)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
        }


        #endregion

        #region States
        public void OnEnter()
        {
            _agent.speed = 0;
        }

        public void OnExit()
        {
            _agent.speed = _movementSpeed;
        }

        public void Tick()
        {
        }

        #endregion

    }
}