using Abstract;
using UnityEngine;

namespace StateMachines.AIBrain.EnemyBrain.BossEnemyBrain.States
{
    public class BossWaitState : IState
    {
        #region Self Variables

        #region Private Variables

        private readonly AIBrains.BossEnemyBrain.BossEnemyBrain _bossEnemyBrain;
        private readonly Animator _animator;
        private readonly string _idle = "Idle";

        #endregion

        #endregion

        public BossWaitState(Animator animator, AIBrains.BossEnemyBrain.BossEnemyBrain bossEnemyBrain)
        {
            _bossEnemyBrain = bossEnemyBrain;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetTrigger(_idle);
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    } 
}
