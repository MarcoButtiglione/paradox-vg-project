using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO dev intermittent
public class DisappearingPlatformController : MonoBehaviour
{
    //[SerializeField] private DisappearingFunctioning _function;
    
    [SerializeField] private DisappearingFunctioning _functioningYoung;
    [SerializeField] private DisappearingFunctioning _functioningOld;

    [Tooltip("Set the period if function is 'Intermittent'. Set function 'Intermittent'.")]
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float _intermittentPeriod = 1f;
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float _intermittentStartingDelay = 1f;
    //The starting state when it is intermitent
    [SerializeField] private bool _startingIntermittentState;
    

    private bool _isIntermittent;

    [Header("Initial state (Young/Old phase)")] [SerializeField]
    private DisappearingState _initYoungState;

    [SerializeField] private DisappearingState _initOldStateOFF;

    [Header("Sprites")] [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    private bool _isActive;


    //-------------------------------
    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider2D = gameObject.GetComponent<Collider2D>();
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
        CancelInvoke();
        if (_functioningYoung == DisappearingFunctioning.Fixed)
        {
            if (_initYoungState == DisappearingState.Active)
            {
                SetActive();
            }
            else if (_initYoungState == DisappearingState.Inactive)
            {
                SetInactive();
            }
        }
        else if (_functioningYoung == DisappearingFunctioning.Intermittent)
        {
            if (_initYoungState == DisappearingState.Active)
            {
                _isIntermittent = true;
                if(_startingIntermittentState){
                    SetActive();
                }
                else
                {
                    SetInactive();
                }
                
                InvokeRepeating("Intermittent", _intermittentPeriod / 2+_intermittentStartingDelay, _intermittentPeriod / 2);
            }
            else if (_initYoungState == DisappearingState.Inactive)
            {
                _isIntermittent = false;
                CancelInvoke();
                SetInactive();
            }
        }
    }

    private void InitOld()
    {
        CancelInvoke();
        if (_functioningOld == DisappearingFunctioning.Fixed)
        {
            if (_initOldStateOFF == DisappearingState.Active)
            {
                SetActive();
            }
            else if (_initOldStateOFF == DisappearingState.Inactive)
            {
                SetInactive();
            }
        }
        else if (_functioningOld == DisappearingFunctioning.Intermittent)
        {
            if (_initOldStateOFF == DisappearingState.Active)
            {
                _isIntermittent = true;
                if(_startingIntermittentState){
                    SetActive();
                }
                else
                {
                    SetInactive();
                }
                InvokeRepeating("Intermittent", _intermittentPeriod / 2+_intermittentStartingDelay, _intermittentPeriod / 2);
            }
            else if (_initOldStateOFF == DisappearingState.Inactive)
            {
                _isIntermittent = false;
                CancelInvoke();
                SetInactive();
            }
        }
    }
    //-------------------------------


    public void SwitchState()
    {
        if (GameManager.Instance.State==GameState.YoungPlayerTurn)
        {
            if (_functioningYoung == DisappearingFunctioning.Fixed)
            {
                if (_isActive)
                {
                    SetInactive();
                }
                else
                {
                    SetActive();
                }
            }
            else if (_functioningYoung == DisappearingFunctioning.Intermittent)
            {
                if (_isIntermittent)
                {
                    _isIntermittent = false;
                    CancelInvoke();
                    SetInactive();
                }
                else
                {
                    _isIntermittent = true;
                    SetActive();
                    InvokeRepeating("Intermittent", _intermittentPeriod / 2, _intermittentPeriod / 2);
                }
            }
        }
        else if (GameManager.Instance.State==GameState.OldPlayerTurn)
        {
            if (_functioningOld == DisappearingFunctioning.Fixed)
            {
                if (_isActive)
                {
                    SetInactive();
                }
                else
                {
                    SetActive();
                }
            }
            else if (_functioningOld == DisappearingFunctioning.Intermittent)
            {
                if (_isIntermittent)
                {
                    _isIntermittent = false;
                    CancelInvoke();
                    SetInactive();
                }
                else
                {
                    _isIntermittent = true;
                    SetActive();
                    InvokeRepeating("Intermittent", _intermittentPeriod / 2, _intermittentPeriod / 2);
                }
            }
        }
        else
        {
            if (_isActive)
            {
                SetInactive();
            }
            else
            {
                SetActive();
            }
        }
        
    }


    private void SetActive()
    {
        _spriteRenderer.sprite = _spriteOn;
        _isActive = true;
        _collider2D.enabled = true;
    }

    private void SetInactive()
    {
        _spriteRenderer.sprite = _spriteOff;
        _isActive = false;
        _collider2D.enabled = false;
    }


    private void Intermittent()
    {
        if (GameManager.Instance.State is not GameState.StartingYoungTurn or GameState.StartingSecondPart
            or GameState.StartingThirdPart or GameState.StartingOldTurn)
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
        }
    }
}


[System.Serializable]
public enum DisappearingState
{
    Inactive,
    Active
}

[System.Serializable]
public enum DisappearingFunctioning
{
    Fixed,
    Intermittent
}