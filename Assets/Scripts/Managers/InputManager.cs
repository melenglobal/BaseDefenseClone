using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion Public Variables

        #region Serialized Variables

        [SerializeField] private FloatingJoystick Joystick;

        [SerializeField] private bool isReadyForTouch,isFirstTimeTouchTaken;


        #endregion Serialized Variables

        #region Private Variables

        private bool _isTouching;

        private float _currentVelocity; //ref type

        private Vector2? _mousePosition; //ref type

        private Vector3 _moveVector; //ref type

        private Vector3 _joystickPosition;
       // public bool IsJoystickMoving => Joystick != null && ();

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            Data = GetInputData();
            
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
         
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
           
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscriptions

        private void Update()
        {   
            if (!isReadyForTouch) return;
            
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }
            IdleInput();
        }

        private void IdleInput()
        {
            if (Input.GetMouseButton(0))
            {
                _isTouching = true;

                if (_isTouching)
                {
                    
                    if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
                    {
                        _joystickPosition = new Vector3(Joystick.Horizontal, 0, Joystick.Vertical);
                        InputSignals.Instance.onJoyStickInputDragged?.Invoke(new InputParams()
                        {
                            InputValues = _joystickPosition
                        });

                    }
                }

            }
        }

        private void OnPlay() => isReadyForTouch = true;

        private void OnEnableInput() => isReadyForTouch = true;

        private void OnDisableInput() => isReadyForTouch = false;

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }

