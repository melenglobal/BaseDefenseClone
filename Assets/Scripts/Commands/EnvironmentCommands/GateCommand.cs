using DG.Tweening;
using UnityEngine;

namespace Commands.EnvironmentCommands
{
    public class GateCommand : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables
        private float _gateAngleZ;
        #endregion

        #endregion

        public void GateOpen(bool GateOpen)
        {
            _gateAngleZ = GateOpen ? -85 : -188;
            transform.DORotate(new Vector3(0,0,_gateAngleZ), 1.2f, 0).SetEase(Ease.OutBounce);
        }
    }
}