using Managers;
using UnityEngine;


namespace Controllers
{
    public class TurretStackPhysicsControl : MonoBehaviour
    {
        private float _timer= 0.4f;
        [SerializeField]
        private AmmoContaynerManager _ammoContaynerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
              
                _ammoContaynerManager.SetTurretStack(other.gameObject.transform.
                    parent.GetComponent<AmmoWorkerStackController>().SendAmmoStack());
            }

        }


        private void OnTriggerStay(Collider other)
        {
          
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
  
                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.08f;
                    
                    _ammoContaynerManager.IsHitAmmoWorker();

                }
            }
            
        }
    }
}