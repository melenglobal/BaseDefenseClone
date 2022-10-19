using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        #region SerilizeField
        
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator cameraAnimator;
        
        #endregion
        #region Private Variables
        
        private Transform _playerTransform;

        #endregion
        #endregion

        #region Event Subcription


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInitialize += OnPlayerInitialize;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterTurret += OnEnterTurret;
            CoreGameSignals.Instance.onFinish += OnFinish;
            CoreGameSignals.Instance.onLevel += OnLevel;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInitialize-= OnPlayerInitialize;
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
        private void OnPlayerInitialize(Transform playerTransform)
        {   
            _playerTransform = playerTransform;
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(_playerTransform);
            ChangeCamera(CameraTypes.Level);
        }
        
        [Button]
        public void OnFinish()
        {   
            stateDrivenCamera.Follow = _playerTransform;
            ChangeCamera(CameraTypes.Finish);
        }

        private void OnEnterTurret()
        {
            ChangeCamera(CameraTypes.Turret);
            stateDrivenCamera.Follow = null;
        }

        private void OnLevel()
        {   
            stateDrivenCamera.Follow = _playerTransform;
            ChangeCamera(CameraTypes.Level);
        }
        private void OnReset()
        {
            ChangeCamera(CameraTypes.Level);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(_playerTransform);
        }

        private void OnSetCameraTarget(Transform playerTransform)
        {
            stateDrivenCamera.Follow = _playerTransform;
            ChangeCamera(CameraTypes.Level);
        }

        private void ChangeCamera(CameraTypes cameraType)
        {
            cameraAnimator.Play(cameraType.ToString());
        }
        
        #endregion
    }
}
