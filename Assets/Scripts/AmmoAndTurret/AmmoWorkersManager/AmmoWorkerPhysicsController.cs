using AIBrain;
using Managers;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {

        private AmmoManager _ammoManager;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();

                _ammoManager.IsAmmoEnterAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerBrain>());
             
                _ammoManager.IsSetTargetTurretContayner(transform.parent.GetComponent<AmmoWorkerBrain>());
            }
            if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
            {
               _ammoManager.IsEnterTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());
            }
        }


        private void OnTriggerExit(Collider other)
        {
           
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();

                _ammoManager.IsAmmoExitAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.ResetItems();
            }
            if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
            {
                _ammoManager.IsExitTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.IsExitOnTurretStack(transform.parent.GetComponent<AmmoWorkerStackController>());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();

                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.1f;
           
                    _ammoManager.IsStayOnAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerBrain>(),
                                                        transform.parent.GetComponent<AmmoWorkerStackController>());
                }
            }

            if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
            {
                _ammoManager.IsAmmoWorkerStackEmpty(transform.parent.GetComponent<AmmoWorkerBrain>());
            }

        }



    }
}