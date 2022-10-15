using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract;
using Controllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class PlayerManager : MonoBehaviour
    {
      #region Self Variables

        #region Public Variables

        public AreaType CurrentAreaType = AreaType.Base;
        
        public WeaponTypes WeaponType;
        
        public List<IDamageable> EnemyList = new List<IDamageable>();
        
        public Transform EnemyTarget;
        
        public bool HasEnemyTarget = false;

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private PlayerMeshController meshController;
        [SerializeField] 
        private PlayerAnimationController animationController;
        [SerializeField] 
        private PlayerWeaponController weaponController;
        [SerializeField] 
        private PlayerShootingController shootingController;
        [SerializeField]
        private PlayerMovementController movementController;
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        public int _health;

        private const int _increaseAmount = 1;

        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _health = _data.Health;
            _weaponData = GetWeaponData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponDatas[(int)WeaponType];
        private void Init() => SetDataToControllers();
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_data.MovementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onJoystickInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
            CoreGameSignals.Instance.onGetHealthValue += OnSetHealthValue;
            CoreGameSignals.Instance.onTakeDamage += OnTakeDamage;
            CoreGameSignals.Instance.onLevelInitialize += OnPlayerInitialize;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onJoystickInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
            CoreGameSignals.Instance.onGetHealthValue -= OnSetHealthValue;
            CoreGameSignals.Instance.onTakeDamage -= OnTakeDamage;
            CoreGameSignals.Instance.onLevelInitialize += OnPlayerInitialize;
        }

        private void OnPlayerInitialize()
        {   
            animationController.gameObject.SetActive(true);
            CoreGameSignals.Instance.onPlayerInitialize?.Invoke(transform);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnGetInputValues(InputParams inputParams)
        {
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            AimEnemy();
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }

        public async void IncreaseHealth()
        {
            if (CurrentAreaType != AreaType.Base)
                return;
      
            if (_data.Health == _health)
            {
                UISignals.Instance.onHealthVisualClose?.Invoke();
                return;
            }
            _health += _increaseAmount;
            UISignals.Instance.onHealthUpdate?.Invoke(_health);
            
            await Task.Delay(50);
            IncreaseHealth();
            
        }

        private void OnTakeDamage(int damage)
        {   
            _health -= damage;
            UISignals.Instance.onHealthUpdate?.Invoke(_health);
            if (_health != 0) return;
            UISignals.Instance.onHealthVisualClose?.Invoke();
            
        }
        private void AimEnemy() => movementController.RotatePlayerToTarget(!HasEnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnimation(bool onTurret) => animationController.HoldTurret(onTurret);

        private int OnSetHealthValue() => _health;

        public void SetOutDoorHealth() => UISignals.Instance.onOutDoorHealthOpen?.Invoke();
        
    
        

    }
}
