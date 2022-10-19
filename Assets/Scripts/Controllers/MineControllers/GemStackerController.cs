using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Abstract.Stackable;
using Abstract.Stacker;
using Signals;
using Task = System.Threading.Tasks.Task;

namespace Controllers
{
    public class GemStackerController : AStacker
    {
        public List<Vector3> PositionList=new List<Vector3>();

        [SerializeField] private float radiusAround;
        
        private float stackDelay = 0.5f;

        private Sequence GetStackSequence;

        private int stackListOrder = 0;

        private int stackListConstCount;

        private bool canRemove = true;
        
        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }
        public new List<GameObject> StackList
        {
            get => base.StackList;
            set => base.StackList = value;
        }
        public override void SetStackHolder(Transform otherTransform)
        {
              otherTransform.SetParent(transform);
        }
        public void GetStack(GameObject stackableObj,Transform otherTransform)
        {
            
            SetStackHolder(otherTransform);
            GetStackSequence = DOTween.Sequence();
            var randomBouncePosition =CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();
            
            GetStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            GetStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);
            
                StackList.Add(stackableObj);
            
                stackableObj.transform.DOLocalMove(PositionList[StackList.Count - 1], 0.3f);
            });
            if (PositionList.Count-1 <= StackList.Count)
            {
                DropzoneSignals.Instance.onDropZoneFull?.Invoke(true);
            }
            
            
        }
        public void OnRemoveAllStack(Transform targetTransform)
        {   
            if(!canRemove)
                return;
            
            
            canRemove = false;
            
            stackListConstCount = StackList.Count;
            
            RemoveAllStack(targetTransform);
        }

        private async void RemoveAllStack(Transform targetTransform)
        {
            
            if (StackList.Count == 0)
            {
                DropzoneSignals.Instance.onDropZoneFull?.Invoke(false);
                canRemove = true;
                return;
            }

            if (StackList.Count <= 0) return;
            RemoveStackAnimation(StackList[StackList.Count - 1],targetTransform);
            StackList.TrimExcess();
            CoreGameSignals.Instance.onUpdateGemScoreData?.Invoke();
            await Task.Delay(100);
            RemoveAllStack(targetTransform);
        }

        private void RemoveStackAnimation(GameObject removedStack,Transform targetTransform)
        {
            GetStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();
            GetStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .1f));
            GetStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .1f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(targetTransform.forward);
                StackList.Remove(removedStack);
                          
                removedStack.transform.DOLocalMove(targetTransform.localPosition+new Vector3(0,targetTransform.localScale.y*2,0), .1f).OnComplete(() =>
                {
                    removedStack.transform.DOScale(Vector3.zero, 0.2f);
                    removedStack.transform.SetParent(null);
                    removedStack.SetActive(false);
                });
            });
        }

        public override void ResetStack(IStackable stackable)
        {
            base.ResetStack(stackable);
        }

        public void GetStackPositions(List<Vector3> stackPositions)
        {
            PositionList = stackPositions;
        }
        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230,310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1,179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX,randomRotationY,randomRotationZ);
        }
    }
}