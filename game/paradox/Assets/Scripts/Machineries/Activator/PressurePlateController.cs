using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField]private float _plateMovement = 0.05f;
    [SerializeField] private GameObject[] _objToActivate;
    
    [Header("Initial state (Young/Old phase)")]
    [HideInInspector][SerializeField] private bool _initYoungState=false;
    [HideInInspector][SerializeField] private bool _initOldState=false;
    
    
    private Vector3 _originalPos;
    private bool _isActive = false;
    private bool _isHover=false;

    void Start()
    {
        _originalPos = transform.position;
    }
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
            transform.position = _originalPos;
            _isActive = true;
            _isHover = true;
        }
        else
        {
            transform.position = _originalPos;
            _isActive = false;
            _isHover = false;
        }
        
    }
    private void InitOld()
    {
        if (_initOldState)
        {
            transform.position = _originalPos;
            _isActive = true;
            _isHover = true;
        }
        else
        {
            transform.position = _originalPos;
            _isActive = false;
            _isHover = false;
        }
    }
    //-------------------------------

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Old"))  
        {
            if (col.contacts[0].normal.y <= -0.9f && Math.Abs(col.contacts[0].normal.x)<0.1f)
            {
                _isHover = true;
                col.gameObject.transform.SetParent(transform);
            }
            
            
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Old"))
        {
            _isHover = false;
            col.gameObject.transform.SetParent(null);
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
    
    void FixedUpdate()
    {
        if (_isHover)
        {
            if (Math.Abs(transform.position.y-_originalPos.y)< _plateMovement)
            {
                transform.Translate(0, -0.01f, 0);
            }
            else
            {
                if (!_isActive)
                {
                    SetActive();
                }   
            }
        }
        else
        {
            if (transform.position.y < _originalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                if (_isActive)
                {
                    SetInactive();
                }
            }
        }
    }
}
