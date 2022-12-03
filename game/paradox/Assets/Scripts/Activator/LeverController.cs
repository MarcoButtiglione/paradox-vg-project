using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _objToActivate;
    
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;

    
    [Header("Sprites")]
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;
    
    private bool _isActive=false;
    
    //-------------------------------
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.StartingYoungTurn)
        {
            InitYoung();
        }
        if (state == GameState.StartingOldTurn)
        {
            InitOld();
        }
    }
    private void InitYoung()
    {
        if (_initYoungState)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
            _isActive = false;
        }
        
    }
    private void InitOld()
    {
        if (_initOldState)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
            _isActive = false;
        }
    }
    //-------------------------------

    private void SetActive()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOn;
        
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<ActivableController>())
            {
                _objToActivate[i].GetComponent<ActivableController>().SwitchState();
            }
        }
    }
    private void SetInactive()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
        
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<ActivableController>())
            {
                _objToActivate[i].GetComponent<ActivableController>().SwitchState();
            }
        }
    }


    public void TriggerLever()
    {
        _isActive = !_isActive;
        if (_isActive)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
        //Play the click sound-----
        FindObjectOfType<AudioManager>().Play("Click");
        //-------------------------
    }
}
