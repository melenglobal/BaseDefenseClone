using Abstract;
using Enum;
using Managers;
using Managers.BaseManagers;
using UnityEngine;

namespace AI.MinerAI.States
{
    public class GemSourceState:IState
    {
        private MinerManager _minerManager;
        public bool IsMiningTimeUp=>_timer>=_minerAIBrain.GemCollectionOffset;
        private MinerAIBrain _minerAIBrain;
        private MinerItems _minerItems;
        private MinerAnimationStates _minerAnimationState;
        private float _timer;
        public GemSourceState(MinerAIBrain minerAIBrain, MinerManager minerManager,
            MinerAnimationStates minerAnimationState, MinerItems minerItems)
        {
            _minerAIBrain = minerAIBrain;
            _minerItems = minerItems;
            _minerManager = minerManager;
            _minerAnimationState = minerAnimationState;
        }

        public void Tick()
        {
            _timer += Time.deltaTime;
        }

        public void OnEnter()
        {
        _minerAIBrain.MinerAIItemController.OpenItem(_minerItems);
        _minerManager.ChangeAnimation(_minerAnimationState);
        
        }

        public void OnExit()
        {
            ResetTimer();
            _minerAIBrain.MinerAIItemController.CloseItem(_minerItems);
            _minerAIBrain.SetTargetForGemHolder();
        }

        private void ResetTimer()
        {
            _timer = 0;
        }
    }
}