using Abstract.Stackable;
using Managers;
using UnityEngine;

namespace Controllers.MilitaryBaseControllers
{
    public class MilitaryBaseTentPhysicsController : MonoBehaviour
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
            // if (manager.IsTentAvailable && TryGetComponent(typeof(ACandidate),out Component candidate))
            // {
            //     manager.UpdateSoldierAmount(_IncreaseAmount);
            // }
        }
    }
}