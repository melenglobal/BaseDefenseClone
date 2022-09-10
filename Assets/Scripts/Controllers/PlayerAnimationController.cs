using System;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        [SerializeField] private PlayerAnimationType playerAnimationType;
        #endregion Serialized Variables

        #region Private Variables

        private int VelocityHash;

        #endregion
        #region Public Variables

        public float velocityZ = 0.0f;
        public float velocityX = 0.0f;
        public float accelaretion = 2f;
        public float decelaretion = 2f;
        public float maximumRunVelocity = 0.5f;
        
        
        

        #endregion
        #endregion Self Variables

        private void Awake()
        {
           // VelocityHash = Animator.StringToHash("Velocity");
        }

        public void ChangeAnimationState(PlayerAnimationType type)
        {
            if (playerAnimationType != type)
            {   
                playerAnimationType = type;
                playerAnimator.Play(type.ToString());
            }
            
        
        }

        private void Update()
        {
            // bool runPressed = Input.GetKey("w");
            // bool leftPressed = Input.GetKey("a");
            // bool rightPressed = Input.GetKey("d");
            //
            // //float currentMaxVelocity = runPressed ? maximumRunVelocity : 
            // // VelocityX ve VelocityY ye inputtan degisken al.
            // // if player presses forward,increase velocity in z direction
            // if (runPressed && velocityZ < 0.5f)
            // {
            //     velocityZ += Time.deltaTime * accelaretion;
            // }
            //
            // // if player presses in left direction
            // if (leftPressed && velocityX> -0.5f && !runPressed)
            // {
            //     velocityX -= Time.deltaTime * accelaretion;
            // }
            // // if player presses in right direction
            // if (rightPressed && velocityX <0.5f && !runPressed)
            // {
            //     velocityX += Time.deltaTime * accelaretion;
            // }
            //
            // if (!runPressed && velocityZ> 0.0f)
            // {
            //     velocityZ -= Time.deltaTime * decelaretion;
            // }
            // if (!runPressed && velocityZ< 0.0f)
            // {
            //     velocityZ = 0.0f;
            // }
            //
            // if (!leftPressed && velocityX <0.0f)
            // {
            //     velocityX += Time.deltaTime * decelaretion;
            // }
            // // decrease velocityX if right is not pressed and velocityX >0
            // if (!rightPressed && velocityX >0.0f)
            // {
            //     velocityX -= Time.deltaTime * decelaretion;
            // }
            // // reset velocityX
            // if (!leftPressed && !rightPressed && velocityX!= 0.0f &&(velocityX > -0.05f && velocityX<0.05f))
            // {
            //     velocityX = 0.0f;
            // }
            // // Set the parameters to our local variable values
            // playerAnimator.SetFloat("VelocityZ",velocityZ);
            // playerAnimator.SetFloat("VelocityX",velocityX);
        }
    }
}