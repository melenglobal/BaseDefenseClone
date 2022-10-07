using Keys;
using UnityEngine;
using Enums;

namespace Controllers
{
  
    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;

        private Vector2 rotateDirection;

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
            
            var angle = Mathf.Atan2(rotateDirection.x,rotateDirection.y) * Mathf.Rad2Deg;
            
            if (!(angle < 60) || !(angle > -60)) return;
            
            transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
        }
    }
}