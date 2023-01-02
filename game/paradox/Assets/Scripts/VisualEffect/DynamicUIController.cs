using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicUIController : MonoBehaviour
{
    private Animator _animatorUI;
    private bool _isUIUp;
    private const float CooldownUIup = 1.0f;
    private float _timerUI;
    private bool _isCooldownUI;
    private static readonly int IsUp = Animator.StringToHash("IsUp");

    private bool _isMoving;

    private void Awake()
    {
        _animatorUI = gameObject.GetComponent<Animator>();
        _isUIUp = true;
        _isCooldownUI = false;
        
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
