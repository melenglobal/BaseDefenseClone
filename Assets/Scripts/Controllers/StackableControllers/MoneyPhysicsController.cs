using Abstract.Interfaces;
using UnityEngine;

namespace Controllers.StackableControllers
{   
    //[RequireComponent(typeof(Collider))]
    public class MoneyPhysicsController : MonoBehaviour,ITriggerEnter
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;
        public GameObject TriggerEnter()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
            return transform.gameObject;
            //collider.isTrigger = false;
        }
    }
}