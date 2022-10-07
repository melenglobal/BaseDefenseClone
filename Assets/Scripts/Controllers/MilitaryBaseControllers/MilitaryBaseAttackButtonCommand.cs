using Managers;
using Signals;
using UnityEngine;

namespace Controllers.MilitaryBaseControllers
{
    public class MilitaryBaseAttackButtonCommand : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private MilitaryBaseManager manager;
        
        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AISignals.Instance.onSoldierActivation?.Invoke();
                Debug.Log("Player");
            }
        }
    }
}