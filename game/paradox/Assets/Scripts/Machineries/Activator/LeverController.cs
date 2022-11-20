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
    
    
    //Functions used by the "activator manager". Initialize the activator in the various phases of the game.
    public void InitYoung()
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
    public void InitOld()
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
        
        //TODO SET ACTIVE
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
        gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
        
        //TODO SET INACTIVE
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
