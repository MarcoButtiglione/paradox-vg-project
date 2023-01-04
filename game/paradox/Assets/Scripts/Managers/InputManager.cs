using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    private bool holdingDown = false;
    public static event Action<DeviceUsed> OnChangedInputDevice;
    private bool _isUsingGamepad;

    private void Start()
    {
        var gamepad = Gamepad.current;
        var joystick = Joystick.current;
        if (gamepad != null ||joystick != null)
        {
            SetGamepad();
        }
        else
        {
            SetKeyboard();
        }
        InputSystem.onAnyButtonPress.Call(
            ctrl =>
            {
                if (ctrl.device is Gamepad or Joystick)
                {
                    if (!_isUsingGamepad)
                    {
                        SetGamepad();
                    }  
                }
                else if (ctrl.device is Keyboard)
                {
                    if (_isUsingGamepad)
                    {
                        SetKeyboard();
                    } 
                }
                    
            });

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.StartingYoungTurn)
        {
            if (Input.anyKey)
            {
                holdingDown = true;
            }

            if (!Input.anyKey && holdingDown)
            {
                GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
                holdingDown = false;
            }

        }

        if (GameManager.Instance.State == GameState.StartingOldTurn && !PostProcessingManager.Instance.isProcessing)
        {
            Time.timeScale = 0f;
            if (Input.anyKey)
            {
                GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
            }
        }
        
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 stickL = gamepad.leftStick.ReadValue();
            if (Math.Abs(stickL.x)>0.1 || Math.Abs(stickL.y)>0.1)
            {
                if (!_isUsingGamepad)
                {
                    SetGamepad();
                }
            }
            Vector2 stickR = gamepad.rightStick.ReadValue();
            if (Math.Abs(stickR.x)>0.1 || Math.Abs(stickR.y)>0.1)
            {
                if (!_isUsingGamepad)
                {
                    SetGamepad();
                }
            }
        }
    }

    private void SetGamepad()
    {
        _isUsingGamepad = true;
        OnChangedInputDevice?.Invoke(DeviceUsed.Gamepad);
    }
    private void SetKeyboard()
    {
        _isUsingGamepad = false;
        OnChangedInputDevice?.Invoke(DeviceUsed.Keyboard);
    }
}

public enum DeviceUsed
{
    Keyboard,
    Gamepad
}