using Abstract;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Managers;
using StateBehaviour;
using States;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.ValueObject;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain
{
    public class AmmoWorkerBrain : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeField Variables

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AmmoManager ammoManager;
        #endregion

        #region Private Variables 

        int counter;
        [SerializeField]
        private bool  _inplaceWorker;
        private bool _isLoadTurretContayner;
        private GameObject _targetTurretContayner;

 
        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;

        private MoveToAvaliableContayner _moveToAvaliableConteyner;

        private PlayerAmmoStackStatus _playerAmmaStackStatus;

        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Create _creat;
        private AmmoWorkerAIData _ammoWorkerAIData;


       

        private StateMachine _statemachine;

        #endregion
        #endregion

        #region GetReferans
        private void Awake()
        {
            InitBrain();
        }
        public void InitBrain()
        {
            _ammoWorkerAIData = Resources.Load<CD_Enemy>("Data/CD_AIData").AmmoWorkerAIData;
            GetStatesReferences();
            TransitionofState();
        }
        public void IsStackFul(PlayerAmmoStackStatus status) => _playerAmmaStackStatus = status;

        public void SetTriggerInfo(bool IsInPlaceWareHouse) => _inplaceWorker = IsInPlaceWareHouse;


        internal void IsLoadTurret(bool isLoadTurretContayner)
        {
            _isLoadTurretContayner = isLoadTurretContayner;
        }

        public void SetTargetTurretContayner(GameObject targetTurretContayner)
        {

            if (targetTurretContayner == null) Debug.LogError("Turret Stack Target Is Null In AmmoWorkerBrain");

            _moveToAvaliableConteyner.SetData(targetTurretContayner);
            _targetTurretContayner = targetTurretContayner;
        }

        internal  void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Create();

            _moveToWareHouse = new MoveToWareHouse(_agent, _animator, _ammoWorkerAIData.Speed, 
                                                    _ammoWorkerAIData.WorkerINitTransform,this);

            _takeAmmo = new TakeAmmo(_agent,_animator);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(_agent, _animator, _ammoWorkerAIData.Speed);

            _loadTurret = new LoadContayner(_agent, _animator, _ammoWorkerAIData.Speed, _ammoWorkerAIData.WorkerINitTransform);

            _fullAmmo = new FullAmmo(_agent, _animator, _ammoWorkerAIData.Speed);

        }

    

        #endregion

        #region StateEngine

        internal  void TransitionofState()
        {
   

            #region Transtion

            At(_creat, _moveToWareHouse, IsAmmoWorkerBorn());

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _moveToAvaliableConteyner,WhenAmmoWorkerStackFull());

            At(_moveToAvaliableConteyner, _loadTurret, IsAmmoWorkerInContayner());

            At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());

            //if (_playerAmmaStackStatus == PlayerAmmaStackStatus.Full)
            //{
            //    _statemachine.AddAnyTransition(_fullAmmo, HasNoEmtyTarget());//bak buna 
            //}

            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> IsAmmoWorkerBorn() => () => _ammoWorkerAIData.WorkerINitTransform.transform != null;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _inplaceWorker == true && _ammoWorkerAIData.WorkerINitTransform.transform != null;

            Func<bool> WhenAmmoWorkerStackFull() => () => _targetTurretContayner != null && _playerAmmaStackStatus == PlayerAmmoStackStatus.Full;

            Func<bool> IsAmmoWorkerInContayner() => () => _targetTurretContayner != null && _isLoadTurretContayner==true;

            Func<bool> WhenAmmoDichargeStack() => () =>  _playerAmmaStackStatus == PlayerAmmoStackStatus.Empty;

            //Func<bool> HasNoEmtyTarget() => () => _targetTurretContayner == null;

            #endregion
        }

        public void Update()
        {
            _statemachine.Tick();
        }
        #endregion



    }
}
