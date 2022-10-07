using Abstract;
using AI.MinerAI;
using Managers;
using UnityEngine;

namespace AI.States
{
    public class DropGemState:IState
    {
        private readonly MinerAIBrain _minerAIBrain;
        public bool IsGemDropped=>isGemDropped;
        private bool isGemDropped;

        public DropGemState(MinerAIBrain minerAIBrain)
        {
            _minerAIBrain = minerAIBrain;
        }

        public void Tick()
        {
        
        }

        public void OnEnter()
        {
            isGemDropped = true;
        }

        public void OnExit()
        {
            _minerAIBrain.SetTargetForMine();
        }
    }
}