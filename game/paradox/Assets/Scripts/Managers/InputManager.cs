using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    private bool holdingDown = false;
    public static event Action<DeviceUsed> OnChangedInputDevice;
    private bool _isUsingGamepad;
    
    private PlayerInputActions _actions;
    private bool _isActionPerfomed;
    
    private void Awake()
    {
        _actions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        _actions.Enable();
        _actions.YoungPlayer.Move.performed += AnyActionPerformed;
        _actions.YoungPlayer.Jump.performed += AnyActionPerformed;
        _actions.YoungPlayer.Restart.performed += AnyActionPerformed;
        _actions.YoungPlayer.Interact.performed += AnyActionPerformed;
        _actions.OldPlayer.Move.performed += AnyActionPerformed;
        _actions.OldPlayer.Jump.performed += AnyActionPerformed;
        _actions.OldPlayer.Restart.performed += AnyActionPerformed;
        _actions.OldPlayer.Interact.performed += AnyActionPerformed;
        _actions.OldPlayer.Dash.performed += AnyActionPerformed;
        _actions.UI.Pause.performed += AnyActionPerformed;

    }
    
    private void OnDisable()
    {
        _actions.Disable();
        _actions.YoungPlayer.Move.performed -= AnyActionPerformed;
        _actions.YoungPlayer.Jump.performed -= AnyActionPerformed;
        _actions.YoungPlayer.Restart.performed -= AnyActionPerformed;
        _actions.YoungPlayer.Interact.performed -= AnyActionPerformed;
        _actions.OldPlayer.Move.performed -= AnyActionPerformed;
        _actions.OldPlayer.Jump.performed -= AnyActionPerformed;
        _actions.OldPlayer.Restart.performed -= AnyActionPerformed;
        _actions.OldPlayer.Interact.performed -= AnyActionPerformed;
        _actions.OldPlayer.Dash.performed -= AnyActionPerformed;
        _actions.UI.Pause.performed -= AnyActionPerformed;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.StartingYoungTurn)
        {
            /*
            if (Input.anyKey)
            {
                holdingDown = true;
            }

            if (!Input.anyKey && holdingDown)
            {
                GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
                holdingDown = false;
            }
            */
            if (_isActionPerfomed)
            {
                GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
            }
            
        }

        if (GameManager.Instance.State == GameState.StartingOldTurn && !PostProcessingManager.Instance.isProcessing)
        {
            Time.timeScale = 0f;
            if (_isActionPerfomed)
            {
                GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
            }
        }
        _isActionPerfomed = false;
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
    private void AnyActionPerformed(InputAction.CallbackContext obj)
    {
        _isActionPerfomed = true;
        if (obj.control.device is Keyboard)
        {
            if (_isUsingGamepad)
                SetKeyboard();
        }
        else
        {
            if(!_isUsingGamepad)
                SetGamepad();
        }
    }
    
}

public enum DeviceUsed
{
    Keyboard,
    Gamepad
}