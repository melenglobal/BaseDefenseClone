using Abstract;
using AIBrains.EnemyBrain;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace AIBrains.BossEnemyBrain.States
{
    public class BossDeathState : IState
    {
        #region Self Variables

        #region Private Variables
        
        private readonly Animator _animator;

        private BossEnemyBrain _bossEnemyBrain;


        #endregion

        #endregion

        public BossDeathState(Animator animator,BossEnemyBrain bossEnemyAIBrain)
        {
            _animator = animator;
            _bossEnemyBrain = bossEnemyAIBrain;
        }
        public void OnEnter()
        {

            _animator.SetTrigger("Death");
            CoreGameSignals.Instance.onOpenPortal?.Invoke();

            DOVirtual.DelayedCall(2f, () => _bossEnemyBrain.gameObject.SetActive(false));

        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            
        }

    } 
}
