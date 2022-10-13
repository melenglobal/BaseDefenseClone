using System;
using System.Collections;
using Abstract;
using Controllers.WorkerPhysicsControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using StateBehaviour;
using StateMachines.AIBrain.Workers.MoneyStates;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.WorkerBrain.MoneyWorker
{
    public class MoneyWorkerAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [BoxGroup("Public Variables")]
        public Transform CurrentTarget;

        #endregion

        #region Private Variables
        
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;
        private MoneyWorkerAIData _moneyWorkerAIData;
        private Vector3 _waitPos;

        #region States
        
        private MoveToGateState _moveToGateState;
        private SearchState _searchState;
        private WaitOnGateState _waitOnGateState;
        private StackMoneyState _stackMoneyState;
        private DropMoneyOnGateState _dropMoneyOnGateState;
        private StateMachine _stateMachine;

        #endregion

        #region Worker Game Variables
        [ShowInInspector]
        private int _currentStock = 0;
        private float _delay = 0.05f;

        #endregion

        #endregion

        #endregion


        private void Awake()
        {
            _moneyWorkerAIData = GetData();
   
        }

        private void Start()
        {
            SetWorkerComponentVariables();
            InitWorker();
            GetReferenceStates();
        }


        #region Data Jobs

        private MoneyWorkerAIData GetData() => Resources.Load<CD_AI>("Data/CD_AI").MoneyWorkerAIData;
        private void SetWorkerComponentVariables()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }
        #endregion

        #region Worker State Jobs

        private void GetReferenceStates()
        {
            _searchState = new SearchState(_navmeshAgent, _animator, this);
            _moveToGateState = new MoveToGateState(_navmeshAgent, _animator, _waitPos,_moneyWorkerAIData.Speed);
            _waitOnGateState = new WaitOnGateState(_navmeshAgent, _animator, this);
            _stackMoneyState = new StackMoneyState(_navmeshAgent, _animator, this,_moneyWorkerAIData.Speed);
            _dropMoneyOnGateState = new DropMoneyOnGateState(_navmeshAgent, _animator, _waitPos);

            _stateMachine = new StateMachine();

            At(_moveToGateState, _searchState, HasArrive());
            At(_searchState, _stackMoneyState, HasCurrentTargetMoney());
            At(_stackMoneyState, _searchState, _stackMoneyState.IsArriveToMoney());
            At(_stackMoneyState, _dropMoneyOnGateState, HasCapacityFull());
            At(_dropMoneyOnGateState, _searchState, HasCapacityNotFull());

            _stateMachine.SetState(_moveToGateState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasArrive() => () => _moveToGateState.IsArrive;
            Func<bool> HasCurrentTargetMoney() => () => CurrentTarget != null;
            Func<bool> HasCapacityFull() => () => !IsAvailable();
            Func<bool> HasCapacityNotFull() => () => IsAvailable();
        }

        private void Update() => _stateMachine.Tick();

        #endregion

        #region General Jobs
        private void InitWorker()
        {

        }
        public bool IsAvailable() => _currentStock < _moneyWorkerAIData.Capacity;

        private void SetTarget()
        {
            CurrentTarget = GetMoneyPosition();
            if (CurrentTarget)
                _navmeshAgent.SetDestination(CurrentTarget.position);
        }
        public void SetInitPosition(Vector3 slotPosition)
        {
            _waitPos = slotPosition;
        }

        private Transform GetMoneyPosition()
        {
            return MoneyWorkerSignals.Instance.onGetTransformMoney?.Invoke(this.transform);
        }

        private IEnumerator SearchTarget()
        {
            while (!CurrentTarget)
            {
                SetTarget();
                yield return new WaitForSeconds(_delay);
            }
        }
        public void StartSearch(bool isStartedSearch)
        {
            if(isStartedSearch)
                StartCoroutine(SearchTarget());
            else
            {
                StopCoroutine(SearchTarget());
            }
        }

        public void SetCurrentStock()
        {
            if (_currentStock < _moneyWorkerAIData.Capacity)
                _currentStock++;
        }

        public void RemoveAllStock()
        {
            for (int i = 0; i <  _moneyWorkerAIData.Capacity; i++)
            {
                if (_currentStock > 0)
                    _currentStock--;
                else
                    _currentStock = 0;
            }
        }
        #endregion

    }
}
