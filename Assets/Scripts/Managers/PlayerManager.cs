using System.Collections.Generic;
using Abstract;
using Controllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion Public Variables

        #region Serialized Variables

        [Space][SerializeField] private PlayerMovementController movementController;
        
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerShootingController shootingController;
        [SerializeField] private PlayerWeaponController weaponController;

        [SerializeField] private InputHandlers InputHandlers = InputHandlers.Character;

        public List<IDamageable> EnemyList = new List<IDamageable>();

        #endregion Serialized Variables

        public AreaType NextAreaType = AreaType.Battle;
        public AreaType currentAreaType = AreaType.Base;
        [SerializeField]
        private WeaponData weaponData;
        
    
        public WeaponTypes WeaponTypes;
        
        public Transform EnemyTarget;
        
        private PlayerAnimationType playerAnimation;

        public bool HasEnemyTarget;
        
        #endregion Self Variables

     
        private void Awake()
        {
            Data = GetPlayerData();
            weaponData = GEtWeaponData();
            SendPlayerDataToControllers();
        }

        private WeaponData GEtWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponDatas[(int)WeaponTypes];
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
            weaponController.SetWeaponData(weaponData);
            meshController.SetWeaponData(weaponData);

        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
            InputSignals.Instance.onJoystickInputDragged += OnUpdateInputParams;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
            InputSignals.Instance.onJoystickInputDragged -= OnUpdateInputParams;
        }
        #endregion
        

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnUpdateInputParams(InputParams inputParams)
        {   
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            if (!HasEnemyTarget) return;
            AimEnemy();
        }

        private void OnDisableMovement(InputHandlers ınputHandlers)
        {
            if (ınputHandlers == InputHandlers.Turret)
            {
                movementController.DisableMovement();
            }
        }

        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }

        private void AimEnemy()
        {
            if (EnemyList.Count == 0) return;
            var transformEnemy = EnemyList[0].GetTransform();
            movementController.RotatePlayerToTarget(transformEnemy);
        }
        private void OnPlay() => movementController.IsReadyToPlay(true);
        

        private void OnReset()
        {
            gameObject.SetActive(false);
        }

        public void CheckAreaStatus(AreaType areaStatus)
        {
            currentAreaType = areaStatus;             
            meshController.ChangeAreaStatus(areaStatus);
        }
        
        //public void IsEnterAmmoCreater(Transform transform) => AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        // public void IsEnterTurret(GameObject turretObj) => movementController.EnterToTurret(turretObj);
        // public void IsExitTurret() => movementController.ExitToTurret();
        

    }
}
