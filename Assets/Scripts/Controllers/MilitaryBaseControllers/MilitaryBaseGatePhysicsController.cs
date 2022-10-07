using System.Collections;
using Abstract.Stackable;
using Managers;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers.MilitaryBaseControllers
{
    public class MilitaryBaseGatePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _IncreaseAmount = 1;

        #endregion

        #region Serialized Variables

        [SerializeField]
        private MilitaryBaseManager manager;
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            // if (manager.IsBaseAvailable && TryGetComponent(typeof(ACandidate),out Component candidate))
            // {
            //     manager.UpdateTotalAmount(_IncreaseAmount);
            //     //Manager.goWaitZone
            // }
        }



        
    }
}