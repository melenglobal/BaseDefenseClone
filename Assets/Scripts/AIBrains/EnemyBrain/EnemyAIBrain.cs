using System;
using Abstract;
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
        
        // Player target,mayin target,taret target
        public Transform target;
        private void Awake()
        {   
            target = FindObjectOfType<PlayerManager>().transform;

        }

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
            At(move,chase,HasTarget()); // player chase range
            At(chase,attack,AttackRange()); // remaining distance < 1f
            At(attack,chase,AttackOffRange()); // remaining distance > 1f
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death, ()=> death.IsDead);
            // At(moveToBomb,attack,() => is);
            _stateMachine.AddAnyTransition(moveToBomb, ()=>isBombSettled);
            
            _stateMachine.SetState(move);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            
            Func<bool> HasTarget() => () => target != null;
            Func<bool> AttackRange() => () => target != null && chase.isPlayerInRange;
            Func<bool> AttackOffRange() => () => target != null && !chase.isPlayerInRange;
            Func<bool> TargetNull() => () => target is null;
        }
        
        
        private void Update() => _stateMachine.Tick();

        public void BombSettled(Transform bombTransform)
        {
            if (!(Vector3.Distance(bombTransform.position, transform.position) < 25f)) return;
            isBombSettled = true;
            target = bombTransform;
        }
    }
}