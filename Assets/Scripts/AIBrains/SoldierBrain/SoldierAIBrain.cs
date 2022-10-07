using System;
using System.Collections.Generic;
using Abstract;
using Abstract.Interfaces.Pool;
using AIBrains.EnemyBrain;
using Controllers;
using Controllers.SoldierPhysicsControllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.WeaponData;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Public Variables
        
        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasEnemyTarget = false;
        
        public Transform TentPosition; 
        public Transform FrontYardStartPosition;
        public List<IDamageable> enemyList = new List<IDamageable>();
        public Transform EnemyTarget;
        #endregion

        #region Serialized Variables

        [SerializeField] private SoldierPhysicsController physicsController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform weaponHolder;
        #endregion

        #region Private Variables
        
        private NavMeshAgent _navMeshAgent;
       
        [ShowInInspector] private List<IDamageable> _damagablesList;
        [Header("Data")]
        private SoldierAIData _data;

        public IDamageable Damageable;
        private int _damage;
        private float _soldierSpeed;
        private float _attackRadius;
        private int _health;
        private StateMachine _stateMachine;
        private Vector3 _slotTransform;
        private bool HasSoldiersActivated;
        private List<WeaponData> _weaponDatas;
      
        
        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
            SetSoldierAIData();
          
        } private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").SoldierAIData;

        private void SetSoldierAIData()
        {
            _damage = _data.Damage;
            _soldierSpeed = _data.Speed;
            _attackRadius = _data.AttackRadius;
            _health = _data.Health;
        } 
        public GameObject GetObject(PoolType poolName)
        {
            var bulletPrefab = CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            bulletPrefab.transform.position = weaponHolder.position;
            bulletPrefab.GetComponent<BulletPhysicsController>().soldierAIBrain = this;
            FireBullet(bulletPrefab);
            return bulletPrefab;
        }
        private void FireBullet(GameObject bulletPrefab)
        {
            bulletPrefab.transform.rotation = _navMeshAgent.transform.rotation;
            var rigidBodyBullet = bulletPrefab.GetComponent<Rigidbody>();
            rigidBodyBullet.AddForce(_navMeshAgent.transform.forward*40,ForceMode.VelocityChange);
        }
        private void GetStateReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            var idle = new Idle(this,TentPosition,_navMeshAgent,animator);
            var moveToWaitZone = new MoveToWaitZone(this,_navMeshAgent,HasReachedSlotTarget,_slotTransform,animator);
            var wait = new Wait(animator,_navMeshAgent);
            var moveToFrontYard = new MoveToFrontYard(this,_navMeshAgent,FrontYardStartPosition,animator);
            var patrol = new Patrol(this,_navMeshAgent,animator);
            var shootTarget = new ShootTarget(this,_navMeshAgent,animator);
            _stateMachine = new StateMachine();
            
            At(idle,moveToWaitZone,hasSlotTransformList());
            At(moveToWaitZone,moveToFrontYard,hasSoldiersActivated());
            At(moveToWaitZone, wait, hasReachToSlot());
            At(wait,moveToFrontYard,hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());
            At(patrol,shootTarget,hasEnemyTarget());
            At(shootTarget,patrol, hasNoEnemyTarget());

            _stateMachine.SetState(idle);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> hasSlotTransformList()=> ()=> _slotTransform!= null;
            Func<bool> hasReachToSlot()=> ()=> _slotTransform != null && HasReachedSlotTarget;
            Func<bool> hasSoldiersActivated()=> ()=> FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard()=> ()=> FrontYardStartPosition != null && HasReachedFrontYard;
            Func<bool> hasEnemyTarget() => () => HasEnemyTarget;
            Func<bool> hasNoEnemyTarget() => () => !HasEnemyTarget;
        }

        private void Update() => _stateMachine.Tick();

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            _slotTransform = slotTransfrom;
        }
        private void OnSoldierActivation()
        {
            HasSoldiersActivated = true;
        }
        public void SetEnemyTargetTransform()
        {
            EnemyTarget = enemyList[0].GetTransform();
            Damageable = enemyList[0];
            HasEnemyTarget = true;
        }

        public void EnemyTargetStatus()
        {
            if (enemyList.Count != 0)
            {
                EnemyTarget = enemyList[0].GetTransform();
                Damageable = enemyList[0];
            }
            else
            {
                HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            if (enemyList.Count == 0) return;
            enemyList.RemoveAt(0);
            enemyList.TrimExcess();
            EnemyTarget = null;
            EnemyTargetStatus();
        }
        
    }
}