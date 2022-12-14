using System;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion Public Variables

        #region Serialized Variables

        [SerializeField] private FloatingJoystick joystick;

        [SerializeField] private bool isReadyForTouch;
        private InputHandlers _inputHandlers = InputHandlers.Character;


        #endregion Serialized Variables

        #region Private Variables

        private bool _isTouching;

        private bool _hasTouched;
        

        #endregion Private Variables

        #endregion Self Variables
        

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
       
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputHandlerChange += OnInputHandlerChange;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputHandlerChange -= OnInputHandlerChange;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscriptions

        private void Update()
        {   
            if (!isReadyForTouch) return;
            if (Input.GetMouseButton(0) && !_hasTouched)
            {
                _hasTouched = true;
            }
            if (!_hasTouched) return;
            
            HandleJoystickInput();

            _hasTouched = joystick.Direction.sqrMagnitude > 0;                         
            
        }

        private void HandleJoystickInput()
        {
            switch (_inputHandlers)
            {
                case InputHandlers.Character:
                    InputSignals.Instance.onJoystickInputDragged?.Invoke(new InputParams()
                    {
                        InputValues = new Vector2(joystick.Horizontal, joystick.Vertical)
                    });
                    break;
                
                case InputHandlers.Turret when joystick.Vertical <= -0.6f:                                      
                    _inputHandlers = InputHandlers.Character;                                                 
                    InputSignals.Instance.onCharacterInputRelease?.Invoke();
                    return;
                
                case InputHandlers.Turret:
                    InputSignals.Instance.onJoystickInputDraggedforTurret?.Invoke(new InputParams()
                    {
                        InputValues = new Vector2(joystick.Horizontal,joystick.Vertical)
                    });
                    if (joystick.Direction.sqrMagnitude != 0)
                    {
                        InputSignals.Instance.onJoystickInputDragged?.Invoke(new InputParams()
                        {
                            InputValues = Vector2.zero
                        });
                    }
                    break;
                
                case InputHandlers.Drone:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnInputHandlerChange(InputHandlers inputHandlers)
        {
            _inputHandlers = inputHandlers;
        }
        private void OnPlay() => isReadyForTouch = true;
        
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
        }
        
    }
}

