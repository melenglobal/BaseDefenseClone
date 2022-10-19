using System;
using System.Collections;
using Abstract.Interfaces.Pool;
using Enums;
using Managers.CoreGameManagers;
using Signals;
using UnityEngine;

namespace Controllers.Bomb
{
    public class BombCollisionController : MonoBehaviour, IReleasePoolObject
    {
        [SerializeField]
        private ParticleSystem bombParticle;

        [SerializeField]
        private GameObject meshObj;

        [SerializeField]
        private Rigidbody bombRB;

        private const float DELAY = 1f;

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool(poolType,obj);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager manager) || collision.gameObject.CompareTag("Ground") )
            {
                StartCoroutine(WaitForTask());
            }

        }
        
        private IEnumerator WaitForTask()
        {
            if (bombParticle)
            {
                bombParticle.Play();
            }

            bombRB.useGravity = false;
            meshObj.SetActive(false);

            yield return new WaitForSeconds(DELAY);

            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = Vector3.one;
            meshObj.SetActive(true);
            ReleaseObject(this.gameObject, PoolType.Bomb);
        }

    
    }
}
