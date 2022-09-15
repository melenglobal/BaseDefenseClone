using System;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Obstacles;
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
        public EnemyData Data;

        #endregion
        
        #region Serialized Variables

        [SerializeField] private Transform spawnPosition;
        
        #endregion

        #region Private Variables
        
        private EnemyType EnemyType;
        private StateMachine _stateMachine;

        #endregion
        

        #endregion
        
        
        
        private void Awake()
        {   
            Data = GetEnemyAIData();
            
            spawnPosition = Data.SpawnPosition; // Levele göre konusucaz
            CurrentTarget = Data.TargetList[Random.Range(0,Data.TargetList.Count)];
            
            TurretTarget = CurrentTarget;
            
            GetStatesReferences();
            
        }

        private EnemyData GetEnemyAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyAIData.EnemyDatas[(int)EnemyType];
        
        private void GetStatesReferences()
        {
            
            var animator = GetComponentInChildren<Animator>();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            
            var search = new Search(this,navMeshAgent,spawnPosition);
            var attack = new Attack(navMeshAgent,animator);
            var move = new Move(this,navMeshAgent,animator);
            var death = new Death(navMeshAgent,animator);
            var chase = new Chase(this,navMeshAgent,animator);
            var moveToBomb = new MoveToBomb(navMeshAgent,animator);
            
            _stateMachine = new StateMachine();
            
            At(search,move,HasInitTarget());
            At(move,chase,HasTargetTurret()); // player chase range
            At(chase,attack,AttackRange()); // remaining distance < 1f
            At(attack,chase,AttackOffRange()); // remaining distance > 1f
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death, ()=> death.IsDead);
            // At(moveToBomb,attack,() => is);
            _stateMachine.AddAnyTransition(moveToBomb, ()=>IsBombSettled);
            
            _stateMachine.SetState(search);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> HasInitTarget() => () => TurretTarget != null;
            Func<bool> HasTargetTurret() => () => CurrentTarget != null && CurrentTarget.TryGetComponent(out PlayerManager player);
            Func<bool> AttackRange() => () => CurrentTarget != null  && Vector3.Distance(transform.position, CurrentTarget.transform.position) < 1f;;
            Func<bool> AttackOffRange() => () => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) > 1f;
            Func<bool> TargetNull() => () => CurrentTarget != null && CurrentTarget.TryGetComponent(out TurretManager turret);
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

            if (CurrentTarget == null)
            {   
                CurrentTarget = TurretTarget;
                return;
            }
        }

        // public void BombSettled(Transform bombTransform)
        // {
        //     if (!(Vector3.Distance(bombTransform.position, transform.position) < 25f)) return;
        //     IsBombSettled = true;
        //     playerTarget = bombTransform;
        // }
    }
}