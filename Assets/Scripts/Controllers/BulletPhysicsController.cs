using Abstract;
using Abstract.Interfaces.Pool;
using AIBrains.SoldierBrain;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour,IReleasePoolObject
    {
        #region Trail Config

        public float Time = 0.5f;
        public float MinVertexDistance = 0.1f;
        public Gradient ColorGradient;
        public Material Material;
        public int CornerVertices;
        public int EndCapVertices;

        #endregion
        
        
        private int bulletDamage = 20;
        
        public SoldierAIBrain soldierAIBrain;
        
        public float AutoDestroyTime = 0.1f;
        
        public float MoveSpeed = 2f;
        
        public int Damage = 5;
        
        public Rigidbody Rigidbody;
        
        protected TrailRenderer Trail;
        
        protected Transform Target;
        
        [SerializeField]
        private Renderer Renderer;

        // private bool IsDisabling = false;

        protected const string DISABLE_METHOD_NAME = "Disable";
        protected const string DO_DISABLE_METHOD_NAME = "DoDisable";

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Trail = GetComponent<TrailRenderer>();
        }
        
        public void SetupTrail(TrailRenderer TrailRenderer)
        {
            TrailRenderer.time = Time;
            TrailRenderer.minVertexDistance = MinVertexDistance;
            TrailRenderer.colorGradient = ColorGradient;
            TrailRenderer.sharedMaterial = Material;
            TrailRenderer.numCornerVertices = CornerVertices;
            TrailRenderer.numCapVertices = EndCapVertices;
        }

        protected virtual void OnEnable()
        {
            if (Renderer != null)
            {
                Renderer.enabled = true;
            }
            //   IsDisabling = false;
            CancelInvoke(DISABLE_METHOD_NAME);
            ConfigureTrail();
            Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
        }
        private void ConfigureTrail()
        {
            if (Trail != null)
            {
                SetupTrail(Trail);
            }
        } 
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if(damagable.IsDead)
                    return;
                var health = damagable.TakeDamage(bulletDamage);
                if (health <= 0)
                {
                    soldierAIBrain.RemoveTarget();
                    Disable();
                }
                else
                {
                    soldierAIBrain.EnemyTargetStatus();
                }
            }
        } 
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
        protected void Disable()
        {
            CancelInvoke(DISABLE_METHOD_NAME);
            CancelInvoke(DO_DISABLE_METHOD_NAME);
            Rigidbody.velocity = Vector3.zero;
            if (Renderer != null)
            {
                Renderer.enabled = false;
            }
            if (Trail != null )
            {
                Invoke(DO_DISABLE_METHOD_NAME, Time);
            }
            else
            {
                DoDisable();
            } 
            ReleaseObject(gameObject,PoolType.PistolBullet);
            gameObject.transform.position = Vector3.zero;
        }
        protected void DoDisable()
        {
            if (Trail != null)
            {
                Trail.Clear();
            }
        }

       
    }
    
}