using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControllerTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _objToActivate;
    
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;

    
    [Header("Sprites")]
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;

    [SerializeField] private GameObject questionMarks;
    
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
        if (GameManager.Instance.State == GameState.SecondPart)
        {
            questionMarks.SetActive(true);
            AudioManager a = FindObjectOfType<AudioManager>();
            if (a)
                a.Play("Click");

            StartCoroutine(activatePlatform());

        }
        else
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
            AudioManager a = FindObjectOfType<AudioManager>();
            if (a)
                a.Play("Click");
            //-------------------------
        }
    }

    IEnumerator activatePlatform()
    {

        //TODO
        //Activate canvas in second part 

        _isActive = !_isActive;
        if (_isActive)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }

        yield return new WaitForSecondsRealtime(2.3f);

        _isActive = !_isActive;
        if (_isActive)
        {
             SetActive();
        }
        else
        {
            SetInactive();
        }
        questionMarks.SetActive(false);
        
        GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);

    }
   
}


