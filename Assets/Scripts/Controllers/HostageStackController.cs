using System;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Enum;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HostageStackController : MonoBehaviour
    {
        #region Self Variables

        public HostageBaseManager hostageBaseManager;
        public Transform Target;
        #endregion

        private void Awake()
        {
            Target=FindObjectOfType<PlayerAnimationController>().transform;
        }


        public void AddHostageStack(MinerManager hostage)
        {
            hostageBaseManager.AddHostageToList(hostage);
        }

        private void FixedUpdate()
        {
             if (hostageBaseManager.StackedHostageList.Count>0&&Target!=null)
             {
                hostageBaseManager.StackedHostageList[0].transform.position = new Vector3(
                    Mathf.Lerp(hostageBaseManager.StackedHostageList[0].transform.position.x, Target.position.x,.2f),
                    Mathf.Lerp(hostageBaseManager.StackedHostageList[0].transform.position.y, Target.position.y,.2f),
                    Mathf.Lerp(hostageBaseManager.StackedHostageList[0].transform.position.z, Target.position.z,.2f));
                hostageBaseManager.StackedHostageList[0].transform.position = Vector3.Lerp(
                    hostageBaseManager.StackedHostageList[0].transform.position
                    , (hostageBaseManager.StackedHostageList[0].transform.position + Target.TransformDirection(0, 0, -1f)),
                    0.2f);
                Quaternion _toPlayerRotation = Quaternion.LookRotation(Target.position - hostageBaseManager.StackedHostageList[0].transform.position);
                 _toPlayerRotation = Quaternion.Euler(0,_toPlayerRotation.eulerAngles.y,0);
                 hostageBaseManager.StackedHostageList[0].transform.rotation = Quaternion.Slerp( Target.rotation,_toPlayerRotation,1f);
            }
             if (hostageBaseManager.StackedHostageList.Count > 1)
            {
                for (int index = 1; index < hostageBaseManager.StackedHostageList.Count; index++)
                {
                    hostageBaseManager.StackedHostageList[index].transform.position = new Vector3(
                        Mathf.Lerp(hostageBaseManager.StackedHostageList[index].transform.position.x, hostageBaseManager.StackedHostageList[index - 1].transform.position.x,.2f),
                        Mathf.Lerp(hostageBaseManager.StackedHostageList[index].transform.position.y, hostageBaseManager.StackedHostageList[index - 1].transform.position.y,.2f),
                        Mathf.Lerp(hostageBaseManager.StackedHostageList[index].transform.position.z, hostageBaseManager.StackedHostageList[index - 1].transform.position.z,.2f));
                    hostageBaseManager.StackedHostageList[index].transform.localPosition= hostageBaseManager.StackedHostageList[index].transform.position + hostageBaseManager.StackedHostageList[index - 1].transform.TransformDirection(0, 0, -0.2f);
                    Quaternion toRotation = Quaternion.LookRotation(hostageBaseManager.StackedHostageList[index - 1].transform.position - hostageBaseManager.StackedHostageList[index].transform.position);
                    toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                    hostageBaseManager.StackedHostageList[index].transform.rotation = Quaternion.Slerp( hostageBaseManager.StackedHostageList[index-1].transform.rotation,toRotation,1f);
                }
            }
        }

        public void SendToGate(Vector3 gate)
        {
            int index = 0;
            foreach (var hostage in hostageBaseManager.StackedHostageList)
            {
                hostage.ChangeAnimation(MinerAnimationStates.Walk);
                hostage.transform.DOMove(gate, 1f + 2 * index / 10f);
                index++;

            }
        }
        public void ClearStack()
        {
            foreach (var hostage in hostageBaseManager.StackedHostageList)
            {
                hostage.minerAIBrain.enabled=true;

            }
            hostageBaseManager.StackedHostageList.Clear();
        }
        
    }
}