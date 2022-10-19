using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class PortalController : MonoBehaviour
    {
        #region Serializable Variables

        [SerializeField]
        private List<MeshRenderer> portalMeshRenderers = new List<MeshRenderer>();

        [SerializeField]
        private Collider portalCollider;

        [SerializeField]
        private float dissolveOpenValue = 6;
        [SerializeField]
        private float dissolveCloseValue = 35;

        private const string _dissolveName = "_DissolveAmount";

        #endregion

        #region Private Variables

        private const float dissolveTime = 2f; 

        #endregion
        
        private void Awake()
        {
            portalCollider.enabled = false;
        }
        public void OpenPortal()
        {
            foreach (var t in portalMeshRenderers)
            {
                t.material.DOFloat(dissolveOpenValue, _dissolveName, dissolveTime);
            }

            DOVirtual.DelayedCall(dissolveTime, () => portalCollider.enabled = true);
        }

        public void ClosePortal()
        {
            foreach (var t in portalMeshRenderers)
            {
                t.material.SetFloat(_dissolveName,dissolveCloseValue);
            }

            portalCollider.enabled = false;
        }
    }
}