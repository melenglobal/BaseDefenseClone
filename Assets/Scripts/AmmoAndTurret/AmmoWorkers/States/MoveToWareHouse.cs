using UnityEngine;
using UnityEngine.AI;
using Abstract;
using AIBrain;

namespace States
{
    public class MoveToWareHouse :IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHouse;
        private AmmoWorkerBrain ammoWorkerBrain;

        public MoveToWareHouse( NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse, AmmoWorkerBrain ammoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHouse = ammoWareHouse;
   
            this.ammoWorkerBrain = ammoWorkerBrain;
        }



        #endregion

        #region State
        public void OnEnter()
        {
            _agent.speed = _movementSpeed;
            _agent.SetDestination(_ammoWareHouse.position);
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            
        } 
        #endregion


    }
}