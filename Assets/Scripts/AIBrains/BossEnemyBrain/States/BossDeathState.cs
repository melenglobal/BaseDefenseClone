using Abstract;
using Enums;
using Signals;
using UnityEngine;

namespace StateMachines.AIBrain.EnemyBrain.BossEnemyBrain.States
{
    public class BossDeathState : IState
    {
        #region Self Variables

        #region Private Variables
        
        private readonly Animator _animator;


        #endregion

        #endregion

        public BossDeathState(Animator animator)
        {
            _animator = animator;
        }
        public void OnEnter()
        {

            _animator.SetTrigger("Death");
            CoreGameSignals.Instance.onOpenPortal?.Invoke();
            //Level Completed
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            
        }

    } 
}
