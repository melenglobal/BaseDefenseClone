using Abstract;
using AI.MinerAI;
using Enum;
using Managers;
using Managers.BaseManagers;
using UnityEngine;


namespace AI.States
{
    public class MoveState:IState
    {
        public bool IsReachedToTarget=>isReachedToTarget;
        private bool isReachedToTarget=false;
        private MinerAnimationStates _minerAnimationStates;
        private MinerItems _minerItems;
        private MinerManager _minerManager;
        private MinerAIBrain _minerAIBrain;
        public MoveState(MinerAIBrain minerAIBrain, MinerManager minerManager, MinerAnimationStates minerAnimationStates,
            MinerItems minerItems)
        {
            _minerAIBrain = minerAIBrain;
            _minerItems = minerItems;
            _minerAnimationStates = minerAnimationStates;
            _minerManager = minerManager;

        }

        public void Tick()
        {
             if (_minerAIBrain.CurrentTarget != null)
             {
                 MoveToTarget();
                RotateToTarget();
                if (_minerAIBrain.transform.position == _minerAIBrain.ManipulatedTarget)
                {
                    isReachedToTarget = true;
                }
                else
                {
                    isReachedToTarget = false;
                }
             }
        }
       
        private void MoveToTarget()
        {
            
            _minerAIBrain.transform.position = Vector3.MoveTowards(_minerAIBrain.transform.position,_minerAIBrain.ManipulatedTarget,0.05f);
        }

        private void RotateToTarget()
        {
            if (_minerAIBrain.CurrentTarget.position- _minerAIBrain.transform.position != Vector3.zero)
            {
                _minerAIBrain.transform.forward =_minerAIBrain.CurrentTarget.position- _minerAIBrain.transform.position;
            }
        }

        public void OnEnter()
        {
            _minerAIBrain.MinerAIItemController.OpenItem(_minerItems);
            _minerManager.ChangeAnimation(_minerAnimationStates);
           
        }
      
        public void OnExit()
        {
           
            isReachedToTarget = false;
        }
    }
}