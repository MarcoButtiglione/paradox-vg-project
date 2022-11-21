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
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;
    
    
    private Vector3 _originalPos;
    private bool _moveBack;
    private bool _isActive = false;
    private bool _isHover=false;

    void Start()
    {
        _originalPos = transform.position;
    }
    //Functions used by the "activator manager". Initialize the activator in the various phases of the game.
    public void InitYoung()
    {
        if (_initYoungState)
        {
            _isActive = true;
            _isHover = true;
        }
        else
        {
            _isActive = false;
            _isHover = false;
        }
        
    }
    public void InitOld()
    {
        if (_initOldState)
        {
            _isActive = true;
            _isHover = true;
        }
        else
        {
            _isActive = false;
            _isHover = false;
        }
    }
    //-------------------------------
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Old"))  
        {
            _isHover = true;
            col.gameObject.transform.SetParent(transform);
            
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Old"))  
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
            if (_objToActivate[i].GetComponent<MovingPlatformController>())
            {
                _objToActivate[i].GetComponent<MovingPlatformController>().SwitchState();
            }
            else
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
               
        }
    }
    private void SetInactive()
    {
        _isActive = false;
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<MovingPlatformController>())
            {
                _objToActivate[i].GetComponent<MovingPlatformController>().SwitchState();
            }
            else
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
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
                _moveBack = false;
            }
        }
    }
}
