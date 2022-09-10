using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        public int ScoreVaryant;

        #endregion Public Variables

        #region Serialized Variables

        [Space][SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        //[SerializeField] private PlayerScoreController playerScoreController;

        [SerializeField] private GameObject scoreHolder;

        [SerializeField] private Rigidbody playerRigidbody;

        [SerializeField] private CapsuleCollider playerCollider;

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
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onJoyStickInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;

        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onJoyStickInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            
        }
        #endregion
        

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnActivateMovement()
        {
            Debug.Log("Movement Activated!");
            ChangePlayerAnimation(PlayerAnimationType.Run);

           movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {   
            Debug.Log("Movement Deactivated!");
            movementController.DeactiveMovement();

            ChangePlayerAnimation(PlayerAnimationType.Idle);
        }

        private void OnGetInputValues(InputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
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

    }
}
