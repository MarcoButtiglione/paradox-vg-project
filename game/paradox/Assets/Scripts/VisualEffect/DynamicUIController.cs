using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class DynamicUIController : MonoBehaviour
{
    private Animator _animatorUI;
    private bool _isUIUp;
    private const float CooldownUIup = 1.0f;
    private float _timerUI;
    private bool _isCooldownUI;
    private static readonly int IsUp = Animator.StringToHash("IsUp");

    [SerializeField] private GameObject escButton;
    [SerializeField] private GameObject escGamePadButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject restartGamePadButton;
    private bool _isUsingGamepad;
    
    private bool _isMoving;

    private void Awake()
    {
        _animatorUI = gameObject.GetComponent<Animator>();
        _isUIUp = true;
        _isCooldownUI = false;
        
        var gamepad = Gamepad.current;
        var joystick = Joystick.current;
        if (gamepad != null ||joystick != null)
        {
            _isUsingGamepad = true;
            escButton.SetActive(false);
            restartButton.SetActive(false);
            escGamePadButton.SetActive(true);
            restartGamePadButton.SetActive(true);
        }
        else
        {
            _isUsingGamepad = false;
            escButton.SetActive(true);
            restartButton.SetActive(true);
            escGamePadButton.SetActive(false);
            restartGamePadButton.SetActive(false);
        }

        InputSystem.onDeviceChange += DeviceChanged;
        InputSystem.onAnyButtonPress.Call(
            ctrl =>
            {
                if (ctrl.device is Gamepad or Joystick)
                {
                    if (!_isUsingGamepad)
                    {
                        _isUsingGamepad = true;
                        escButton.SetActive(false);
                        restartButton.SetActive(false);
                        escGamePadButton.SetActive(true);
                        restartGamePadButton.SetActive(true);
                
                    }  
                }
                else if (ctrl.device is Keyboard)
                {
                    if (_isUsingGamepad)
                    {
                        _isUsingGamepad = false;
                        escButton.SetActive(true);
                        restartButton.SetActive(true);
                        escGamePadButton.SetActive(false);
                        restartGamePadButton.SetActive(false);
                    } 
                }
                    
            });


        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void DeviceChanged(InputDevice arg1, InputDeviceChange arg2)
    {
        Debug.Log(arg1);
        Debug.Log(arg2);
    }

    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state is GameState.StartingYoungTurn or GameState.StartingThirdPart or GameState.StartingSecondPart or GameState.StartingOldTurn)
        {
            _isMoving = false;
            _isUIUp = true;
            _isCooldownUI = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {

        //UI TRIGGER UP DOWN
        if (_isMoving)
        {
            _isUIUp = false;
            _isCooldownUI = false;
        }
        else if(!_isCooldownUI&&!_isUIUp)
        {
            _timerUI = CooldownUIup;
            _isCooldownUI = true;
        }

        if (_isCooldownUI)
        {
            _timerUI -= Time.deltaTime;
            if (_timerUI < 0) 
            {
                _isUIUp = true;
                _isCooldownUI = false;
            }
        }

        _animatorUI.SetBool(IsUp,_isUIUp);
    }

    public void SetMoving(bool b)
    {
        _isMoving = b;
    }
}
