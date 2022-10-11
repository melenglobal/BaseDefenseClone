using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private new Rigidbody rigidbody;
        
        [SerializeField] private PlayerManager manager;

        #endregion Serialized Variables

        #region Private Variables
        
        private PlayerMovementData _movementData;

        private bool _isReadyToMove, _isReadyToPlay, _isMovingVertical;

        private float _inputValueX;

        private bool _hasEnemyTarget;

        private Vector2 _inputVector;

        private Vector3 _movementDirection;
        
        #endregion Private Variables

        #endregion Self Variables

        public void SetMovementData(PlayerMovementData dataMovementData) => _movementData = dataMovementData;
        
        public void IsReadyToPlay(bool state) => _isReadyToPlay = state;
        
        private void FixedUpdate()
        {
            PlayerMove();
        }
        public void UpdateInputValues(InputParams inputParams)
        {
            _inputVector = inputParams.InputValues;
            EnableMovement(_inputVector.sqrMagnitude > 0);
            
            if (!_hasEnemyTarget)
            {
                
            }
        }
        
        private void PlayerMove()
        {
            if (_isReadyToMove)
            {
                var velocity = rigidbody.velocity; 
                velocity = new Vector3(_inputVector.x,velocity.y, _inputVector.y)*_movementData.Speed;
                rigidbody.velocity = velocity;
                if (!manager.HasEnemyTarget)
                {
                    RotatePlayer();
                }
            }
            else if(rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
        private void RotatePlayer()
        {
            Vector3 movementDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, toRotation,30);
        }

        public void RotatePlayerToTarget(Transform enemyTarget)
        {
            transform.LookAt(enemyTarget,Vector3.up * 3f);
        }

        private void EnableMovement(bool movementStatus)
        {
            _isReadyToMove = movementStatus;
        }

        public void DisableMovement()
        {
            rigidbody.velocity = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }
}