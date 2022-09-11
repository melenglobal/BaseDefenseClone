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
        
        #endregion
        #endregion Self Variables
        
        public void ChangeAnimationState(PlayerAnimationType type)
        {
            if (playerAnimationType != type)
            {   
                playerAnimationType = type;
                playerAnimator.Play(type.ToString());
            }
            
        
        }
    }
}