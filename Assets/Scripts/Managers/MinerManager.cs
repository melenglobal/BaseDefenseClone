using AI.MinerAI;
using Enum;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public MinerAIBrain minerAIBrain;
        
        #endregion

        #region Serialized Variables
        
        public HostageType CurrentType=HostageType.HostageWaiting; 
        [SerializeField] private Animator animator;


        #endregion

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private Transform _currentMinePlace;


        #endregion

        #endregion
        private void Awake()
        {
            //_currentMinePlace=mineBaseManager.GetRandomMineTarget();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DropzoneSignals.Instance.onDropZoneFull += OnDropZoneFull;
        }

        private void OnDropZoneFull(bool _state)
        {
            minerAIBrain.IsDropZoneFullStatus=_state;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            DropzoneSignals.Instance.onDropZoneFull -= OnDropZoneFull;
        }

        #endregion

        public void ChangeAnimation(MinerAnimationStates minerAnimationStates)
        {
            animator.SetTrigger(minerAnimationStates.ToString());
        }

        public void AddToHostageStack()
        {
            CurrentType = HostageType.Hostage;
            HostageSignals.Instance.onAddHostageStack?.Invoke(this);
        }
    }
}