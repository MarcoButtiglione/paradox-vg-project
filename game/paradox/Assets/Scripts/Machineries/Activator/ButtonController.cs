using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    
    [SerializeField] private float _timer = 1.0f;
    [SerializeField] private GameObject[] _objToActivate;
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;
    
    private bool _isActive=false;
    
    //Functions used by the "activator manager". Initialize the activator in the various phases of the game.
    public void InitYoung()
    {
        if (_initYoungState)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
        
    }
    public void InitOld()
    {
        if (_initOldState)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }
    //-------------------------------

    private void SetActive()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOn;
        
        //TODO SET ACTIVE
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
        //gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
        
        //TODO SET INACTIVE
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

    
    public void TriggerButtom()
    {
        StartCoroutine(TriggerButtomCor());
    }
    
    
    IEnumerator TriggerButtomCor()
    {
        if (!_isActive)
        {
            SetActive();
            //Play the click sound-----
            FindObjectOfType<AudioManager>().Play("Click");
            //------------------------
            yield return new WaitForSeconds(_timer);
            SetInactive();
        }
        
    }
}
