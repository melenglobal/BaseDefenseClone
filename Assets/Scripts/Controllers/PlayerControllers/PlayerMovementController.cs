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

        [SerializeField] private PlayerManager manager;

        private bool _isReadyToMove, _isReadyToPlay, _isMovingVertical;

        private float _inputValueX;

        private bool HasEnemyTarget;

        private Vector2 _InputVector;

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
            _InputVector = inputParams.InputValues;
            EnableMovement(_InputVector.sqrMagnitude > 0);
            
            if (!HasEnemyTarget)
            {
                RotatePlayer(inputParams);
            }
        }
        
        private void PlayerMove()
        {
            if (_isReadyToMove)
            {
                var velocity = rigidbody.velocity; 
                velocity = new Vector3(_InputVector.x,velocity.y, _InputVector.y)*_movementData.Speed;
                rigidbody.velocity = velocity;
            }
            else if(rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
        private void RotatePlayer(InputParams inputParams)
        {
            Vector3 movementDirection = new Vector3(inputParams.InputValues.x, 0, inputParams.InputValues.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 30);
        }
        
        public void RotateThePlayer(Transform enemyTransform)
        {
            transform.LookAt(enemyTransform, Vector3.up*3f);
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