using System.Collections;
using UnityEngine;
using DG.Tweening;
using Enums;
using Datas.ValueObject;
using System;

namespace Controllers
{
    public class TurretShootController : MonoBehaviour
    {

        private TurretShootData _gattalingRotateDatas;


        public void SetGattalingRotateDatas(TurretShootData gattalingRotateDatas) => _gattalingRotateDatas = gattalingRotateDatas;

        private void Spin(GattalingActivateStatus Activatable)
        {
            transform.Rotate((int)Activatable*Time.deltaTime* _gattalingRotateDatas.RotateSpeed, 0, 0);

        }

        public void ActiveGattaling() => Spin(GattalingActivateStatus.Active);
        public void DeactiveGattaling() => Spin(GattalingActivateStatus.Pasive);


    }
}