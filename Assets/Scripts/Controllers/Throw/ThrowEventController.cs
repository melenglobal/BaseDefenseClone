using Abstract.Interfaces.Pool;
using AIBrains.BossEnemyBrain;
using Controllers.Bomb;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using StateMachines.AIBrain.EnemyBrain.BossEnemyBrain;
using UnityEngine;

namespace Controllers.Throw
{
    public class ThrowEventController : MonoBehaviour, IReleasePoolObject, IGetPoolObject
    {
        /// <summary>
        /// Call Animation Event
        /// </summary>

        #region Self Variables

        #region Private Variables

        private GameObject _throwBomb;
        private ThrowData _throwData;
        #endregion 

        #region Serializable Variables

        [SerializeField]
        private Transform bombHolder;

        [SerializeField]
        private BossEnemyBrain bossBrain;

        [SerializeField]
        private BombPhysicController bombPhysicController;

        [SerializeField]
        private bool isPathActiveRunTime = true;

        public GameObject SpriteTarget;

        #endregion

        #endregion

        private void Awake()
        {
            _throwData = Resources.Load<CD_Throw>("Data/CD_Throw").ThrowData;
        }

        public void ThrowFunc()
        {
            if (_throwBomb)
            {
                _throwBomb.transform.SetParent(null);
                Throw();
            }
        }

        public void SetAnimationWithBomb()
        {

            if (bossBrain.PlayerTarget)
                bossBrain.transform.LookAt(bossBrain.PlayerTarget, Vector3.up);

            _throwBomb = GetObject(PoolType.Bomb);
            var rb = _throwBomb.GetComponent<Rigidbody>();
            rb.useGravity = false;
            _throwBomb.transform.SetParent(bombHolder);
            _throwBomb.transform.localPosition = Vector3.zero;
            _throwBomb.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            //ReleaseObject(throwBomb,PoolType.Bomb);
        }

        private void Throw()
        {
            SpriteTarget.SetActive(true);
            SpriteTarget.transform.position = bossBrain.PlayerTarget.position + new Vector3(0, 0.2f, 0);
            _throwBomb.transform.SetParent(null);
            var rb = _throwBomb.GetComponent<Rigidbody>();
            Physics.gravity = Vector3.up * _throwData.Gravity;
            rb.useGravity = true;
            rb.velocity = CalculateThrowData().initialVelocity;
        }

        private ThrowInputData CalculateThrowData()
        {
            float distY = bossBrain.PlayerTarget.position.y - _throwBomb.transform.position.y; // y (yukseklik)'de ki yer degistirme
            Vector3 distXZ = new Vector3(bossBrain.PlayerTarget.position.x - _throwBomb.transform.position.x, 0, bossBrain.PlayerTarget.position.z - _throwBomb.transform.position.z); //  x and z yer degistirme
            float time = Mathf.Sqrt(-2 * _throwData.Height / _throwData.Gravity) + Mathf.Sqrt(2 * (distY - _throwData.Height) / _throwData.Gravity); // kok (-2 * yukseklik / yercekimi kuvveti) + kok (2* (yukseklik farkı - offset yukseklik) / yercekimi kuvveti)
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _throwData.Gravity * _throwData.Height); // y de ki velocity hesabi
            Vector3 velocityXZ = distXZ / time; // zamana bagli olarak alacagi yol 

            DOVirtual.DelayedCall(time ,() => DeactiveSpriteTargetDelay());

            return new ThrowInputData(velocityXZ + velocityY * -Mathf.Sign(_throwData.Gravity), time);
        }
        private void DeactiveSpriteTargetDelay()
        {
            if (bombPhysicController)
            {
                bombPhysicController.enabled = true;
                bombPhysicController.enabled = false;
            }
            if (SpriteTarget.activeInHierarchy)
            {
                SpriteTarget.SetActive(false);
            }
            if (_throwBomb!= null)
            {
                ReleaseObject(_throwBomb,PoolType.Bomb);
            }
        }

        /*
    private void DrawPath()
    {
        ThrowInputData launchData = CalculateThrowData();
        Vector3 previousDrawPoint = _throwBomb.transform.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * _throwData.Gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = _throwBomb.transform.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }*/

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolType, obj);
        }

        public GameObject GetObject(PoolType poolType)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
        }


    }
}
