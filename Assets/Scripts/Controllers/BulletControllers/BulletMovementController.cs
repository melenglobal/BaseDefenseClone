using DG.Tweening;
using UnityEngine;

namespace Controllers.BulletControllers
{
    public class BulletMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private new Rigidbody rigidbody;

        private const float fireDelay = 0.05f;
        private const float fireSpeed = 70;

        private bool bulletHasFired=false;
        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnEnable()
        {
            DOVirtual.DelayedCall(fireDelay, () => FireBullet());
        }

        private void FireBullet()
        {
            rigidbody.AddRelativeForce(Vector3.forward * fireSpeed, ForceMode.VelocityChange);
            bulletHasFired = false;
        }
        private void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}