using System.Collections.Generic;
using Enums;
using Enums.Player;
using Keys;
using Managers;
using Managers.CoreGameManagers;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
       #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        
        [SerializeField] private Animator animator;
        
        #endregion

        #region Private Variables
        
        private PlayerAnimationStates _currentAnimationState;

        private float _velocityX, _velocityZ;

        private float _acceleration, _decelaration;

        private Dictionary<WeaponTypes, PlayerAnimationStates> _animationStatesMap;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
            DefineDictionary();
        }
        private void DefineDictionary()
        {
            _animationStatesMap = new Dictionary<WeaponTypes, PlayerAnimationStates>()
            {
                {WeaponTypes.PistolBullet, PlayerAnimationStates.Pistol},
                {WeaponTypes.RifleBullet, PlayerAnimationStates.Riffle},
                {WeaponTypes.PumpBullet, PlayerAnimationStates.ShotGun},
                {WeaponTypes.MiniGunBullet, PlayerAnimationStates.MiniGun},
            };
        }
        private void Init()
        {
            animator = GetComponent<Animator>();
        }
        public void PlayAnimation(InputParams inputParams)
        { 
            if (playerManager.CurrentAreaType == AreaType.Battle)
            {
                animator.SetLayerWeight(1,1);
                animator.SetBool("IsBattleOn",true);
                ChangeAnimations(_animationStatesMap[playerManager.WeaponType]);
                animator.SetBool("Aimed",true);
                _velocityX = inputParams.InputValues.x;
                _velocityZ = inputParams.InputValues.y;
                if (_velocityZ < 0.1f)
                {
                    _velocityZ += Time.deltaTime * _acceleration;
                }
                if (_velocityX > -0.1f && Mathf.Abs(_velocityZ) <= 0.2f)
                {
                    _velocityX -= Time.deltaTime * _acceleration;
                }
                if (_velocityX < 0.1f && Mathf.Abs(_velocityZ) <= 0.2f)
                {
                    _velocityX += Time.deltaTime * _acceleration;
                }
                if (_velocityZ > 0.0f)
                {
                    _velocityZ -= Time.deltaTime * _decelaration;
                }
                if (_velocityX < 0.0f)
                {
                    _velocityX += Time.deltaTime * _decelaration;
                }
                if (_velocityX > 0.0f)
                {
                    _velocityX -= Time.deltaTime * _decelaration;
                }
                if ( _velocityX!= 0.0f &&(_velocityX > -0.05f && _velocityX<0.05f))
                {
                    _velocityX = 0.0f;
                }
                animator.SetFloat("VelocityZ",_velocityZ);
                animator.SetFloat("VelocityX",_velocityX);
                if (inputParams.InputValues.sqrMagnitude == 0)
                {
                    AimTarget(playerManager.EnemyTarget);
                }
            }
            else
            {
                animator.SetBool("Aimed",false);
                animator.SetLayerWeight(1,0);
                animator.SetBool("IsBattleOn",false);
                ChangeAnimations( inputParams.InputValues.sqrMagnitude > 0
                    ? PlayerAnimationStates.Run
                    : PlayerAnimationStates.Idle);
            }
        }
        private void ChangeAnimations(PlayerAnimationStates animationStates)
        {
            if (animationStates == _currentAnimationState) return;
             animator.Play(animationStates.ToString());
            _currentAnimationState = animationStates;
        } 
        public void AimTarget(bool hasTarget)
        {
            animator.SetBool("Aimed",hasTarget);
        }

        public void HoldTurret(bool hold)
        {
            animator.SetLayerWeight(2,hold ? 1:0);
        }
    }
    }
