using Cinemachine;
using DG.Tweening;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
    #region Self Variables
        #region SerilizeField
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Animator cameraAnimator;
        #endregion
        #region Private Variables

        #endregion
        #endregion

        #region Event Subcription


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterTurret += OnEnterTurret;
            CoreGameSignals.Instance.onFinish += OnFinish;
            CoreGameSignals.Instance.onLevel += OnLevel;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterTurret -= OnEnterTurret;
            CoreGameSignals.Instance.onFinish -= OnFinish;
            CoreGameSignals.Instance.onLevel -= OnLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Evet Methods
        private void OnPlay()
        {
            ChangeCamera(CameraTypes.Level);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }
        
        [Button]
        public void OnFinish()
        {   
            stateDrivenCamera.Follow = playerManager.transform;
            ChangeCamera(CameraTypes.Finish);
        }

        private void OnEnterTurret()
        {
            ChangeCamera(CameraTypes.Turret);
            stateDrivenCamera.Follow = null;
        }

        private void OnLevel()
        {   
            stateDrivenCamera.Follow = playerManager.transform;
            ChangeCamera(CameraTypes.Level);
        }
        private void OnReset()
        {
           ChangeCamera(CameraTypes.Level);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }

        private void OnSetCameraTarget()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            stateDrivenCamera.Follow = playerManager.transform;
            ChangeCamera(CameraTypes.Level);
        }

        private void ChangeCamera(CameraTypes cameraType)
        {
            cameraAnimator.Play(cameraType.ToString());
        }
        
        #endregion
    }
}
