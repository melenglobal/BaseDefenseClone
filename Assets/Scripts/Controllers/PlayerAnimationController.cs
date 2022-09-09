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