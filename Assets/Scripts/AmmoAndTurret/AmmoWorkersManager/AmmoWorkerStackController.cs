using Enums;
using UnityEngine;
using DG.Tweening;
using Managers;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Controllers
{
 
    public class AmmoWorkerStackController:MonoBehaviour
    {


        private float yPos=-0.5f;//passed false
        private float zPos=-0.25f;
        private Sequence ammoSeq;
        
        [SerializeField]
        private List<GameObject> ammoStackObjectList=new List<GameObject>();

        public void AddStack(Transform startPoint, Transform ammoWorker, GameObject bullets)
        {
            ammoSeq = DOTween.Sequence();
            bullets.transform.position = startPoint.position;
            bullets.transform.SetParent(ammoWorker);

            ammoSeq.Append(bullets.transform.DOScale(new Vector3(1, 1, 1), 0.8f));


            ammoSeq.Join(bullets.transform.DOLocalMove(new Vector3(Random.Range(0, 2), bullets.transform.localPosition.y +

                   Random.Range(4, 5), bullets.transform.localPosition.z - Random.Range(3, 4)), 0.4f).
                   OnComplete(()
                   => {
                            
                            
                            bullets.transform.DOLocalMove(new Vector3(0, ammoWorker.localPosition.y + yPos + 1.5f, -ammoWorker.localScale.z - zPos), 0.4f);

                            yPos += 0.5f;
                      }));


                ammoSeq.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179),Random.Range(-179, 179), Random.Range(-90, 90)), 0.3f).

                    OnComplete(()=> bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.4f)));



            //Debug.Log(zPos);

            ammoSeq.Play();

            

            if (yPos >= 5)
            { 
           
                zPos += 0.5f;
                yPos = 0;
            }
         
            ammoStackObjectList.Add(bullets);

        }
        public List<GameObject> SendAmmoStack()
        {   
            return ammoStackObjectList;
        }
        public void SetClearWorkerStackList()
        {
            zPos = -0.25f;

            ammoStackObjectList.Clear();
            ammoStackObjectList.TrimExcess();
        }
    }
}