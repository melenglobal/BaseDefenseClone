using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private new Rigidbody rigidbody;

        #endregion Serialized Variables

        #region Private Variables

        [Header("Data")] private PlayerMovementData _movementData;

        private bool _isReadyToMove, _isReadyToPlay, _isMovingVertical;

        private float _inputValueX;

        private Vector2 _clampValues;

        private Vector3 _movementDirection;
        

        #endregion Private Variables

        #endregion Self Variables

        public void SetMovementData(PlayerMovementData dataMovementData) => _movementData = dataMovementData;

        public void EnableMovement() => _isReadyToMove = true;

        public void DeactiveMovement() => _isReadyToMove = false;


        public void UpdateInputValue(InputParams inputParam)
        {
            _movementDirection = inputParam.InputValues;
        }

        public void IsReadyToPlay(bool state) => _isReadyToPlay = state;
        

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
    
                   IdleMove();
                   
                  
                }
                else
                {
   
                 
                  Stop();
                    
                }
            }
            else
                Stop();
        }
        
        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_movementDirection.x * _movementData.Speed, velocity.y,
                _movementDirection.z * _movementData.Speed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(rigidbody.position.x, (position = rigidbody.position).y, position.z);
            rigidbody.position = position;

            if (_movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_movementDirection);

                transform.GetChild(0).rotation = toRotation;
            }
        }


        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;

            rigidbody.angularVelocity = Vector3.zero;
            _isReadyToMove = false;
        }
        

        public void MovementReset()
        {
            Stop();

            _isReadyToPlay = false;

            _isReadyToMove = false;

            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
        }
        
    }
}