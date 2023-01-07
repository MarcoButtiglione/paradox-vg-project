using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _firstPart;
    [SerializeField] private GameObject _secondPart;
    [SerializeField] private GameObject _thirdPart;
    [SerializeField] private GameObject _forthPart;
    [SerializeField] private GameObject questionMarks;

    [SerializeField] private GameObject[] keyboardTips;
    [SerializeField] private GameObject[] gamepadTips;
    
    //Event managment 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        InputManager.OnChangedInputDevice += InputManagerOnChangedInputDevice;
        
    }

    private void Start()
    {
        _firstPart.SetActive(true);
        _secondPart.SetActive(false);
        _thirdPart.SetActive(false);
        _forthPart.SetActive(false);
    }
    private void InputManagerOnChangedInputDevice(DeviceUsed obj)
    {
        switch (obj)
        {
            case DeviceUsed.Gamepad:
                foreach (var o in keyboardTips)
                {
                    o.SetActive(false);
                }
                foreach (var o in gamepadTips)
                {
                    o.SetActive(true);
                }
               
                break;
            case DeviceUsed.Keyboard:
                foreach (var o in keyboardTips)
                {
                    o.SetActive(true);
                }
                foreach (var o in gamepadTips)
                {
                    o.SetActive(false);
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
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.StartingYoungTurn)
        {
            _firstPart.SetActive(true);
            _secondPart.SetActive(false);
            _thirdPart.SetActive(false);
            _forthPart.SetActive(false);
        }
        else if (state == GameState.StartingSecondPart)
        {
            _firstPart.SetActive(false);
            _secondPart.SetActive(true);
            _thirdPart.SetActive(false);
            _forthPart.SetActive(false);
            
        }
        else if (state == GameState.StartingThirdPart)
        {
            _firstPart.SetActive(false);
            _secondPart.SetActive(false);
            _thirdPart.SetActive(true);
            _forthPart.SetActive(false);
        }
        else if (state == GameState.StartingOldTurn)
        {
            _firstPart.SetActive(false);
            _secondPart.SetActive(false);
            _thirdPart.SetActive(false);
            _forthPart.SetActive(true);
        }
    }

}
