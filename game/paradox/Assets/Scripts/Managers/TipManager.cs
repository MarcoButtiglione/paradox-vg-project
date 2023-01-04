using System;
using UnityEngine;
public class TipManager : MonoBehaviour
{
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;
    [SerializeField] private bool isGamepad;
    private bool _isYoungPart;
    private bool _isUsingPad;
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        InputManager.OnChangedInputDevice += InputManagerOnChangedInputDevice;

    }
    private void InputManagerOnChangedInputDevice(DeviceUsed obj)
    {
        switch (obj)
        {
            case DeviceUsed.Gamepad:
                _isUsingPad = true;
                if ((_isYoungPart&&_initYoungState)||(!_isYoungPart&&_initOldState))
                {
                    gameObject.SetActive(isGamepad);
                }
                break;
            case DeviceUsed.Keyboard:
                _isUsingPad = false;
                if ((_isYoungPart&&_initYoungState)||(!_isYoungPart&&_initOldState))
                {
                    gameObject.SetActive(!isGamepad);
                }
                break;
        }
        
    }


    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.StartingYoungTurn:
                _isYoungPart = true;
                if (_isUsingPad&&isGamepad)
                {
                    gameObject.SetActive(_initYoungState);
                }
                else if (!_isUsingPad&&!isGamepad)
                {
                    gameObject.SetActive(_initYoungState);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                
                
                break; 
            case GameState.StartingOldTurn:
                _isYoungPart = false;
                if (_isUsingPad&&isGamepad)
                {
                    gameObject.SetActive(_initOldState);
                }
                else if (!_isUsingPad&&!isGamepad)
                {
                    gameObject.SetActive(_initOldState);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        InputManager.OnChangedInputDevice -= InputManagerOnChangedInputDevice;

    }

}
