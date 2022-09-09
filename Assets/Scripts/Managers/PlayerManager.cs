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
            // CoreGameSignals.Instance.onFailed += OnFailed;
     
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onJoyStickInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            // CoreGameSignals.Instance.onFailed -= OnFailed;
  
        }
        #endregion
        

        public void PlayerPhysicDisabled()
        {
            playerCollider.enabled = false;
            playerRigidbody.useGravity = false;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnActivateMovement()
        {
           ChangePlayerAnimation(PlayerAnimationType.Run);

           movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();

            ChangePlayerAnimation(PlayerAnimationType.Idle);
        }

        // public void ExitPaymentArea()
        // {
        //     ParticalSignals.Instance.onParticleStop?.Invoke();
        // }
        

        private void OnGetInputValues(InputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }
        
        

        private void OnPlay() => movementController.IsReadyToPlay(true);

       // private void OnFailed() => movementController.IsReadyToPlay(false);

        private void OnReset()
        {
            //movementController.MovementReset();
            gameObject.SetActive(false);//changed
           // movementController.ChangeHorizontalSpeed(HorizontalSpeedStatus.Active);
        }
        

        // private void OnEnterIdleArea()
        // {
        //     movementController.ChangeHorizontalSpeed(HorizontalSpeedStatus.Active);
        // }
        //

        // public void IsHitCollectable()
        // {
        //     if (_gameStates == GameStates.Idle)
        //     {
        //         ScoreSignals.Instance.onUpdateScore?.Invoke(ScoreStatus.plus);
        //     }
        // }

        // private void OnSetScoreText(int score)
        // {
        //     ScoreVaryant = score;
        //     playerMeshController.CalculateSmallerRate(score);
        //     PlayerScoreText(score);
        // }
        //private void PlayerScoreText(int score)=> playerScoreController.UpdateScore(score);
       // public void OnStopVerticalMovement() => movementController.StopVerticalMovement();

        

        public void ChangePlayerAnimation(PlayerAnimationType animType)
        {
            animationController.ChangeAnimationState(animType);
        }

    }
}
