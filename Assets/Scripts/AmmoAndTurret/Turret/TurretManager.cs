using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;
namespace Managers
{
    public class TurretManager : MonoBehaviour
    {

        #region Self Variables
        #region SerializeField Variables

        [SerializeField] private GameObject player;
        
        [SerializeField]
        private List<TurretMovementController> turretMovementControllers = new List<TurretMovementController>(6);

        private TurretMovementController _currentMovementController;
        
        // [SerializeField]
        // private TurretOtoAtackController _otoAtackController;
        // [SerializeField]
        // private TurretShootController ShootController;
        #endregion

        #region Private Variables
        private TurretData turretData;
        #endregion

        #endregion

        #region Get&SetData
        private void Awake() => Init();

        private void Init()
        {
            // turretData = GetTurretData();
            // SetMovementData();
            // OtoAtackData();
            // GattalingRotateData();
        }
        // private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").turretDatas;
        //
        // private void SetMovementData() => _movementController.SetMovementDatas(turretData.MovementDatas);
        //
        // private void OtoAtackData() => _otoAtackController.SetOtoAtackDatas(turretData.TurretOtoAtackDatas);
        //
        // private void GattalingRotateData() => ShootController.SetGattalingRotateDatas(turretData.gattalingRotateDatas);

  
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCurrentTurret += GetCurrentTurretMovementController;
            InputSignals.Instance.onJoystickInputDraggedforTurret += OnGetInputValues;
            CoreGameSignals.Instance.onCharacterInputRelease += OnCharacterRelease;
            // TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
            // TurretSignals.Instance.onDeadEnemy += OnDeadEnemy;
        }

        private void UnsubscribeEvents()
        {   
            
            CoreGameSignals.Instance.onSetCurrentTurret -= GetCurrentTurretMovementController;
            InputSignals.Instance.onJoystickInputDraggedforTurret -= OnGetInputValues;
            CoreGameSignals.Instance.onCharacterInputRelease -= OnCharacterRelease;
            // TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;
            // TurretSignals.Instance.onDeadEnemy -= OnDeadEnemy;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region SubsciribeMethods
        public void OnPressTurretButton()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }

        private void OnDeadEnemy() => IsEnemyExitTurretRange();

        #endregion

        #region BotController
        public void IsFollowEnemyInTurretRange()
        {
            // ShootController.ActiveGattaling();
            // //transform.GetComponentInChildren<AmmoContaynerManager>().IsTurretAttack();
            // _otoAtackController.FollowToEnemy();
        }

        // public void IsEnemyEnterTurretRange(GameObject enemy) => _otoAtackController.AddDeathList(enemy);
        public void IsEnemyExitTurretRange()
        {
            // _otoAtackController.RemoveDeathList();
            // ShootController.DeactiveGattaling();
        } 
        #endregion
        
        private void CharacterParentChange()
        {
            player.transform.SetParent(_currentMovementController.transform);
            var controllerTransform = _currentMovementController.transform;
            Vector3 turretPos = controllerTransform.position;
            player.transform.position = new Vector3(turretPos.x, transform.position.y, turretPos.z - 2f);
            player.transform.parent = controllerTransform;
        }
        private void OnCharacterRelease()
        {
            player.transform.SetParent(null);
            _currentMovementController.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        private void GetCurrentTurretMovementController(TurretLocationType type) 
        {                                                      
            _currentMovementController = turretMovementControllers[(int)type];
            CharacterParentChange();
        }
        
        private void OnGetInputValues(InputParams value)
        {
            _currentMovementController?.SetInputParams(value);
        }

    }
}