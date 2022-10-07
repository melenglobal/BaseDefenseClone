using System.Collections.Generic;
using Controllers;
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

        [SerializeField] private InputHandlers InputHandlers = InputHandlers.Character;
        

        #endregion Serialized Variables


        private PlayerAnimationType playerAnimation;
        

        #endregion Self Variables

     
        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
           
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
            CoreGameSignals.Instance.onInputHandlerChange += OnDisableMovement;
            InputSignals.Instance.onJoystickInputDragged += OnUpdateInputParams;

        }

        private void UnsubscribeEvents()
        {
     
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onInputHandlerChange -= OnDisableMovement;
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
           
        }

        private void OnDisableMovement(InputHandlers ınputHandlers)
        {
            if (ınputHandlers == InputHandlers.Turret)
            {
                movementController.DisableMovement();
                //movementController.enabled = false;
            }
            
        }

        private void OnPlay() => movementController.IsReadyToPlay(true);
        

        private void OnReset()
        {
            gameObject.SetActive(false);
        }
        
    
        public void ChangePlayerAnimation(PlayerAnimationType animType)
        {
            animationController.ChangeAnimationState(animType);
        }
        public void IsEnterAmmoCreater(Transform transform) => AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        // public void IsEnterTurret(GameObject turretObj) => movementController.EnterToTurret(turretObj);
        // public void IsExitTurret() => movementController.ExitToTurret();
        

    }
}
