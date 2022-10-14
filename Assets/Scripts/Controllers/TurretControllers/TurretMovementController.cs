using Enums;
using Keys;
using UnityEngine;

namespace Controllers.TurretControllers
{
  
    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;
        
        private Vector2 rotateDirection;

        private float _rotationSpeed = 15f;

        [SerializeField] private TurretLocationType turretLocationType;
        
        public void SetInputParams(InputParams input)
        {
            _horizontalInput = input.InputValues.x;
            _verticalInput = input.InputValues.y;
            Rotate();
        }
        
        private void Rotate()
        {
            rotateDirection = new Vector2(_horizontalInput, _verticalInput).normalized;
            if (rotateDirection.sqrMagnitude == 0)
                return;
            
            float angle = Mathf.Atan2(rotateDirection.x,rotateDirection.y) * Mathf.Rad2Deg;
            
            if (!(angle < 60) || !(angle > -60)) return;
            
            transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
            
        }
    }
}