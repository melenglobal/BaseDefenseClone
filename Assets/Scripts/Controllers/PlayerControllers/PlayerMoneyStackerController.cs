using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract.Stackable;
using Abstract.Stacker;
using DG.Tweening;
using Enums;
using Managers.CoreGameManagers;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    [RequireComponent(typeof(PlayerStackController))]
    public class PlayerMoneyStackerController : AStacker
    {
        [SerializeField] private List<Vector3> positionList;

        [SerializeField] private float radiusAround;

        [SerializeField]
        private Transform PoolHolder;
        
        private float _stackDelay = 0.5f;

        private Sequence _getStackSequence;

        private int _stackListOrder = 0;

        private int _stackListConstCount;

        private bool _canRemove = true;

        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }
        private new List<GameObject> StackList = new List<GameObject>();
        public override void SetStackHolder(Transform otherTransform)
        {
           otherTransform.SetParent(transform);
        }
        public override void GetStack(GameObject stackableObj)
        {   
            SetStackHolder(stackableObj.transform);
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition =CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();
            
            _getStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            _getStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);
            
                StackList.Add(stackableObj);

                stackableObj.transform.DOLocalMove(positionList[StackList.Count - 1], 0.3f);
                
                stackableObj.transform.localRotation = new Quaternion(0, 0, 0, 0).normalized;
                
            });
            
        }
        public void OnRemoveAllStack()
        {
            if(!_canRemove)
                return;
            _canRemove = false;
            _stackListConstCount = StackList.Count;
            RemoveAllStack();
        }

        public async void RemoveAllStack()
        {
            if (StackList.Count == 0)
            {
                _canRemove = true;
                return;
            }
            if(StackList.Count >= _stackListConstCount -6)
            {
                RemoveStackAnimation(StackList[StackList.Count - 1]);
                StackList.TrimExcess();
                CoreGameSignals.Instance.onUpdateMoneyScoreData?.Invoke();
                await Task.Delay(201);
                RemoveAllStack();
            }
            else
            {
                for (int i = 0; i < StackList.Count; i++)
                {   
                    RemoveStackAnimation(StackList[i]);
                    StackList.TrimExcess();
                    CoreGameSignals.Instance.onUpdateMoneyScoreData?.Invoke();
                }
                _canRemove = true;
            }
        }

        private void RemoveStackAnimation(GameObject removedStack)
        {
            _getStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();
            
            _getStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .2f));
            _getStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .2f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackList.Remove(removedStack);
                removedStack.transform.DOLocalMove(transform.localPosition, .1f).OnComplete(() =>
                {
                    CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(PoolType.Money,removedStack);
                });
            });
        }

        public async void ResetStack()
        {
            if (StackList.Count == 0)
            {
                return;
            }
            StackList[0].transform.SetParent(null);
            StackList.Remove(StackList[0]);
            StackList.TrimExcess();
            await Task.Delay(10);
            ResetStack();
        }
        public void PaymentStackAnimation(Transform transform)
        {
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPositionWithObjTransform();
            var randomRotation = CalculateRandomStackRotation();
            var moneyObj = CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(PoolType.Money);
            moneyObj.transform.position = this.transform.parent.transform.position;
            moneyObj.GetComponent<Collider>().enabled = false;
            _getStackSequence.Append(moneyObj.transform.DOMove(randomBouncePosition, .5f));
            _getStackSequence.Join(moneyObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                moneyObj.transform.rotation = Quaternion.LookRotation(transform.forward);
                moneyObj.transform.DOMove(transform.position, 0.3f).OnComplete(() => CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(PoolType.Money,moneyObj));

            });
        }

        public void GetStackPositions(List<Vector3> stackPositions)
        {
            positionList = stackPositions;
        }
        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230,310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1,179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX,randomRotationY,randomRotationZ);
        }
        private Vector3 CalculateRandomAddStackPositionWithObjTransform()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230, 310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(transform.parent.position.x + radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, transform.parent.position.z + -radiusAround * Mathf.Sin(rad));
        }
    }
}