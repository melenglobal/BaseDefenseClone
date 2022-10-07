using Datas.ValueObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class TurretOtoAtackController : MonoBehaviour
    {
        #region Self Variabels
        #region Private Variables
        private Queue<GameObject> _deadList = new Queue<GameObject>();
        private GameObject botTarget;
        private TurretOtoAtackData _turretOtoAtackDatas;

        #endregion
        #endregion
        public void SetOtoAtackDatas(TurretOtoAtackData turretOtoAtackDatas) => _turretOtoAtackDatas = turretOtoAtackDatas;
        public void AddDeathList(GameObject enemy)
        {
            _deadList.Enqueue(enemy);
            botTarget = _deadList.Peek();
        }
        public void RemoveDeathList()
        {
            if (_deadList.Count != 0) 
            {
                _deadList.Dequeue();
                botTarget = _deadList.Peek();
            }
            
        }
        public void FollowToEnemy()
        {

            Rotate(botTarget.transform.position);
        }

        private void Rotate(Vector3 _movementDirection)
        {
           
            if (_movementDirection == Vector3.zero) return;
            Vector3 horizontalRotation=new Vector3(_movementDirection.x,0,_movementDirection.z);
            Vector3 _relativePos = horizontalRotation - transform.position;
            Quaternion _toRotation = Quaternion.LookRotation(_relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, Mathf.Lerp(0, 1, _turretOtoAtackDatas.TimeScale));
           // transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, 10);
            Debug.DrawLine(transform.position, transform.forward*15f, Color.red, 0.1f);
        }

       

    }
}