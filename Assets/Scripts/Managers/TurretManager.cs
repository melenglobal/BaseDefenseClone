using System;
using System.Collections.Generic;
using Controllers;
using Controllers.PlayerControllers;
using Controllers.TurretControllers;
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

        [SerializeField] private WeaponTypes weaponTypes = WeaponTypes.TurretBullet;



        // [SerializeField]
        // private TurretOtoAtackController _otoAtackController;
        // [SerializeField]
        // private TurretShootController ShootController;
        #endregion

        #region Private Variables
        
        private TurretMovementController _currentMovementController;
        
        
        #endregion

        #endregion
        

        #region Event Subscription
        private void OnEnable() => SubscribeEvents(); 

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCurrentTurret += OnGetCurrentTurretMovementController;
            InputSignals.Instance.onJoystickInputDraggedforTurret += OnGetInputValues;
            InputSignals.Instance.onCharacterInputRelease += OnCharacterRelease;
            // TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
            // TurretSignals.Instance.onDeadEnemy += OnDeadEnemy;
        }

        private void UnsubscribeEvents()
        {   
            
            CoreGameSignals.Instance.onSetCurrentTurret -= OnGetCurrentTurretMovementController;
            InputSignals.Instance.onJoystickInputDraggedforTurret -= OnGetInputValues;
            InputSignals.Instance.onCharacterInputRelease -= OnCharacterRelease;
            // TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;
            // TurretSignals.Instance.onDeadEnemy -= OnDeadEnemy;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion
        
        #region BotController
        public void IsFollowEnemyInTurretRange()
        {
            // ShootController.ActiveGattaling();
            // //transform.GetComponentInChildren<AmmoContaynerManager>().IsTurretAttack();
            // _otoAtackController.FollowToEnemy();
        }

   
        #endregion

        #region Character on the Turret

        private void CharacterParentChange()  
        {
            player.transform.SetParent(_currentMovementController.transform);
            var controllerTransform = _currentMovementController.transform;
            CoreGameSignals.Instance.onEnterTurret?.Invoke();
            Vector3 turretPos = controllerTransform.position;
            player.transform.position = new Vector3(turretPos.x, transform.position.y, turretPos.z - 2f);
            player.transform.parent = controllerTransform;
        }
        private void OnCharacterRelease()
        {
            player.transform.SetParent(null);
            CoreGameSignals.Instance.onLevel?.Invoke();
            _currentMovementController.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        private void OnGetCurrentTurretMovementController(TurretLocationType type,GameObject _player)
        {
            player = _player;
            _currentMovementController = turretMovementControllers[(int)type];
            CharacterParentChange();
        }
        
        private void OnGetInputValues(InputParams value) => _currentMovementController?.SetInputParams(value);

        #endregion 
      

    }
}