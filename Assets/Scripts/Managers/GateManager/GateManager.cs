using Controllers;
using UnityEngine;

namespace Managers.GateManager
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GatePhysicsController gatePhysicsController;
        //[SerializeField] private GateAnimationController gateAnimationController;
        
        #endregion
        

        #endregion

        public void PlayGateAnimation(bool _state)
        {
            //gateAnimationController.PlayGateAnimation(_state);
        }
    }
}