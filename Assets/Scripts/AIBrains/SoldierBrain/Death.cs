using Abstract;
using Abstract.Interfaces.Pool;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class Death : IState,IReleasePoolObject
    {
        private Animator _soldierAnimator;
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private PoolType poolType;
        public Death(SoldierAIBrain soldierAIBrain, Animator soldierAnimator,NavMeshAgent navMeshAgent)
        {
            _soldierAIBrain = soldierAIBrain;
            _soldierAnimator = soldierAnimator;
            _navMeshAgent = navMeshAgent;
            poolType = PoolType.SoldierAI;
        }
        public void Tick()
        {

        }
        public void OnEnter()
        {
            SoldierDead();
            _soldierAnimator.SetTrigger("Die");
        }
        public void OnExit()
        {

        }
        private void SoldierDead()
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), this.poolType.ToString());
            DOVirtual.DelayedCall(1.5f, () =>
            {
                _soldierAIBrain.transform.DOMoveY(-3f,1f).OnComplete(()=> ReleaseObject(_soldierAIBrain.gameObject,poolType));
            });
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
    }
}