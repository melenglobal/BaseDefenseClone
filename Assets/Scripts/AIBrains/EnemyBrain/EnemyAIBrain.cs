using System;
using Abstract;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Obstacles;
using StateBehaviour;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class EnemyAIBrain : MonoBehaviour
    {
        private StateMachine _stateMachine;

        public bool isBombSettled;

        public NavMeshAgent navMeshAgent;

        public EnemyType EnemyType;
        
        // Player playerTarget,mayin playerTarget,taret playerTarget
        public Transform _currentTarget;
        public Transform _TurretTarget;

        public EnemyData Data;
        private void Awake()
        {   
            //playerTarget = FindObjectOfType<PlayerManager>().transform;
            Data = GetEnemyAIData();
            _TurretTarget = _currentTarget;
            GetStatesReferences();
            
            
        }

        private EnemyData GetEnemyAIData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyAIData.EnemyDatas[(int)EnemyType];

        private void Start()
        {   
            GetStatesReferences();
        }

        private void GetStatesReferences()
        {
            
            var animator = GetComponent<Animator>();
            
            var attack = new Attack(navMeshAgent,animator);
            var move = new Move(this,navMeshAgent,animator);
            var death = new Death(navMeshAgent,animator);
            var chase = new Chase(this,navMeshAgent,animator);
            var moveToBomb = new MoveToBomb(navMeshAgent,animator);
            
            _stateMachine = new StateMachine();
            At(move,chase,HasTargetTurret()); // player chase range
            At(chase,attack,AttackRange()); // remaining distance < 1f
            At(attack,chase,AttackOffRange()); // remaining distance > 1f
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death, ()=> death.IsDead);
            // At(moveToBomb,attack,() => is);
            _stateMachine.AddAnyTransition(moveToBomb, ()=>isBombSettled);
            
            _stateMachine.SetState(move);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            
            Func<bool> HasTargetTurret() => () => _currentTarget != null && _currentTarget.TryGetComponent(out PlayerManager player);
            Func<bool> AttackRange() => () => _currentTarget != null  || Vector3.Distance(transform.position, _currentTarget.transform.position) < 1f;;
            Func<bool> AttackOffRange() => () => _currentTarget != null && Vector3.Distance(transform.position, _currentTarget.transform.position) < 5f;
            Func<bool> TargetNull() => () => _currentTarget == _TurretTarget && _currentTarget.TryGetComponent(out TurretManager turret);
        }


        private void Update()
        {
            _stateMachine.Tick();
            
        }

        public void SetTarget(Transform target)
        {
            if (target == _currentTarget)
            {
                return;
            }
            
            _currentTarget = target;
            
            if (_currentTarget == null)
            {   
                _currentTarget = _TurretTarget;
                Debug.Log(_currentTarget);
                return;
            }
            

        }

        // public void BombSettled(Transform bombTransform)
        // {
        //     if (!(Vector3.Distance(bombTransform.position, transform.position) < 25f)) return;
        //     isBombSettled = true;
        //     playerTarget = bombTransform;
        // }
    }
}