using Abstract;
using Abstract.Interfaces.Pool;
using Enums;
using Signals;
using UnityEngine;

namespace AIBrains.BossEnemyBrain.States
{
    public class BossAttackState : IState, IReleasePoolObject
    {
        #region Self Variables

        #region Private Variables

        private readonly BossEnemyBrain _bossEnemyBrain;
        private readonly Animator _animator;
        private readonly float _attackRange;
        private readonly Transform _bombHolder;
        private readonly string _attack = "Attack";

        #endregion

        #endregion
        public BossAttackState(Animator animator, BossEnemyBrain bossEnemyBrain, float attackRange,ref Transform bombHolder )
        {
            _bossEnemyBrain = bossEnemyBrain;
            _animator = animator;
            _attackRange = attackRange;
            _bombHolder = bombHolder;
        }

        public void OnEnter()
        {
            
            _animator.SetTrigger(_attack);
        }

        public void OnExit()
        {
            if (_bombHolder.childCount > 0)
                ReleaseObject(_bombHolder.GetChild(0).gameObject, PoolType.Bomb);
        }

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolType,obj);
        }

        public void Tick()
        {
           // _bossEnemyBrain.transform.LookAt(_bossEnemyBrain.PlayerTarget, Vector3.up);
        }

    } 
}
