using Data.UnityObject;
using DG.Tweening;
using Managers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerStackController :MonoBehaviour
    {
        [SerializeField]
        private AmmoContaynerManager _ammoContaynerManager;
        [SerializeField]
        private List<GameObject> _ammoWorkerStackList;
        [SerializeField]
        private List<GameObject> _turretContayner=new List<GameObject>();
        public int _currentCount;
        private int _count =0;


        private Sequence _ammoStackingMovement;

        public async void AddStack(List<Vector3> gridPosList)
        {
            _ammoStackingMovement = DOTween.Sequence();
           
            if (_currentCount < gridPosList.Count)
            {
                if (_count < _ammoWorkerStackList.Count)
                {   
                  
                    _ammoWorkerStackList[_count].transform.SetParent(transform);


                    GameObject bullets = _ammoWorkerStackList[_count];


                    Vector3 endPosOnTurretStack = transform.localPosition + gridPosList[_currentCount];


                    _ammoStackingMovement.Append(bullets.transform.
                    DOLocalMove(new Vector3(Random.Range(-2, 2), endPosOnTurretStack.y +
                    Random.Range(4, 6), bullets.transform.localPosition.z+3f ), 0.4f).
                    OnComplete(() =>{bullets.transform.
                    DOMove(new Vector3(endPosOnTurretStack.x, endPosOnTurretStack.y+0.25f , endPosOnTurretStack.z ), 0.4f);}));


                    _ammoStackingMovement.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179), Random.Range(-179, 179), Random.Range(-179, 179)), 0.6f).
                    OnComplete(() => bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f)));
              

                    _ammoStackingMovement.Play();

                    _turretContayner.Add(_ammoWorkerStackList[_count]);

                    
                    _currentCount++;
                    _count++;
                }
                else
                {
                    _count = 0;
                    _ammoWorkerStackList.Clear();
                    _ammoWorkerStackList.TrimExcess();
                    _ammoContaynerManager.SelectTarget();


                    await Task.Delay(10);

                    _ammoContaynerManager.SendToTargetInfo();
                }
            }
            
        }
        
        public void RemoveStack()
        {


        }
        public void SetAmmoWorkerList(List<GameObject> ammoWorkerStackList)
        {
            
            ammoWorkerStackList.Reverse();
            _ammoWorkerStackList = ammoWorkerStackList;
        }

        public int GetCurrentCount()
        {
            return _currentCount;
        }
    }
}