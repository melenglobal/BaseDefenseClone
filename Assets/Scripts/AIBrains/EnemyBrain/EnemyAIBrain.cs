using System;
using System.Collections.Generic;
using Abstract;
using Controllers.PlayerControllers;
using Controllers.SoldierPhysicsControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Managers.CoreGameManagers;
using Signals;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIBrains.EnemyBrain
{
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsBombSettled;
        public Transform CurrentTarget;
        public Transform TurretTarget;

        public bool EnemyReachedBase { get; set; }
        public int Health { get => _health; set => _health = value; } 

        #endregion
        
        #region Serialized Variables

        [SerializeField] 
        private Transform spawnPosition;
        
        [SerializeField]
        private EnemyType enemyType;

        [SerializeField] 
        private NavMeshAgent navMeshAgent;
        
        [SerializeField] 
        private Animator animator;
        

        #endregion

        #region Private Variables

        private const int _enemyAttackPower = 10;
        private int _health;
        private EnemyData _data;
        private StateMachine _stateMachine;
        private Search _search; 
        public PlayerPhysicsController PlayerPhysicsController { get => _playerPhysicsController; set => _playerPhysicsController = value; }
        public SoldierHealthController SoldierHealthController { get => _soldierHealthController; set => _soldierHealthController = value; }
        
        private PlayerPhysicsController _playerPhysicsController;
        private  SoldierHealthController _soldierHealthController;

        #endregion
        
        #endregion
        
        private void Awake()
        {   
            _data = GetEnemyAIData();
            
            Health = _data.Health;
            
            spawnPosition = AISignals.Instance.getSpawnTransform?.Invoke();
            
            CurrentTarget = AISignals.Instance.getRandomTransform?.Invoke();
            
            GetStatesReferences();
        }

        private void OnEnable()
        {
            
            TurretTarget = CurrentTarget;
            
            Health= _data.Health;
            _stateMachine.SetState(_search);
        }
        
        private EnemyData GetEnemyAIData() => Resources.Load<CD_AI>("Data/CD_AI").EnemyAIData.EnemyDatas[(int)enemyType];
        
        private void GetStatesReferences()
        {
            _search = new Search(this,navMeshAgent,spawnPosition);
            var attack = new Attack(navMeshAgent,animator,this);
            var move = new Move(this,navMeshAgent,animator);
            var death = new Death(navMeshAgent,animator,this,enemyType);
            var chase = new Chase(this,navMeshAgent,animator);
            var moveToBomb = new MoveToBomb(navMeshAgent,animator);
            var baseAttack = new BaseAttack(navMeshAgent, animator);
            
            _stateMachine = new StateMachine();
            
            At(_search,move,HasInitTarget());
            At(move,chase,HasTarget()); 
            At(chase,attack,AttackRange()); 
            At(attack,chase,AttackOffRange()); 
            At(chase,move,TargetNull());
            At(move,baseAttack,IsEnemyReachedBase());
            At(baseAttack,chase,IsTargetChange());

            _stateMachine.AddAnyTransition(death,  IsDead());
            _stateMachine.AddAnyTransition(moveToBomb, ()=>IsBombSettled);
            
            _stateMachine.SetState(_search);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> HasInitTarget() => () => TurretTarget != null;
            Func<bool> HasTarget() => () => CurrentTarget != null && (CurrentTarget.TryGetComponent(out PlayerPhysicsController playerPhysicsController) || CurrentTarget.TryGetComponent(out SoldierHealthController soldierHealthController)); ;
            Func<bool> AttackRange() => () => CurrentTarget != null  && (transform.position - CurrentTarget.transform.position).sqrMagnitude < Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> AttackOffRange() => () => CurrentTarget != null && (transform.position - CurrentTarget.transform.position).sqrMagnitude > Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> TargetNull() => () => CurrentTarget == TurretTarget;
            Func<bool> IsDead() => () => Health <= 0;
            Func<bool> IsEnemyReachedBase() => () => CurrentTarget == TurretTarget && (transform.position - CurrentTarget.transform.position).sqrMagnitude < Mathf.Pow(navMeshAgent.stoppingDistance,2);
            Func<bool> IsTargetChange() => () => CurrentTarget != TurretTarget;
        }
        
        private void Update()
        {   
            _stateMachine.Tick();
        }
        
        public void SetTarget(Transform target)
        {
            if (target == CurrentTarget)
            {
                return;
            }
            
            CurrentTarget = target;

            if (CurrentTarget != null) return;
            CurrentTarget = TurretTarget;
            _soldierHealthController = null;
            _playerPhysicsController = null;
        }
        
        public void CacheSoldier(SoldierHealthController soldierHealthController)
        {
            if (soldierHealthController != null && soldierHealthController == _soldierHealthController) return;
            _soldierHealthController = soldierHealthController;
            
        } 
        public void CachePlayer(PlayerPhysicsController playerPhysicsController)
        {
            _playerPhysicsController = playerPhysicsController;
        }
        public void HitDamage()
        {
            if (_soldierHealthController != null)
            {
                int soldierHealth = _soldierHealthController.TakeDamage(_enemyAttackPower);
                if (soldierHealth > 0) return;
                _soldierHealthController = null;
                SetTarget(TurretTarget);
            }
            if (_playerPhysicsController == null) return;
            CoreGameSignals.Instance.onTakePlayerDamage?.Invoke(_enemyAttackPower);
        }

    }
}