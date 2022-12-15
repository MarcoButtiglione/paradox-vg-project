using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField] private GameObject[] _objToActivate;
    
    [Header("Initial state (Young/Old phase)")]
    [HideInInspector][SerializeField] private bool _initYoungState=false;
    [HideInInspector][SerializeField] private bool _initOldState=false;
    
    private bool _isActive = false;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;
    private SpriteRenderer _spriteRenderer;
    
    //-------------------------------
    private void Awake()
    {
        _spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
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
            _spriteRenderer.sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            _spriteRenderer.sprite = _spriteOff;
            _isActive = false;
        }
    }
    private void InitOld()
    {
        if (_initOldState)
        {
            _spriteRenderer.sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            _spriteRenderer.sprite = _spriteOff;
            _isActive = false;
        }
    }
    //-------------------------------

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Old"))
        {
            SetActive();
            _spriteRenderer.sprite = _spriteOn;
            //Play the click sound-----
            FindObjectOfType<AudioManager>().Play("Click");
            //------------------------
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Old"))
        {
            SetInactive();
            _spriteRenderer.sprite = _spriteOff;
            //Play the click sound-----
            FindObjectOfType<AudioManager>().Play("Click");
            //------------------------
        }
    }
    
    private void SetActive()
    {
        _isActive = true;
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
        _isActive = false;
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<ActivableController>())
            {
                _objToActivate[i].GetComponent<ActivableController>().SwitchState();
            }
        }
    }
    
}
