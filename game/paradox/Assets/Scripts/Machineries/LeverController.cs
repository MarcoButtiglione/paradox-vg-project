using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private bool _isActive=false;
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject[] _objToActivate;
    

    public void TriggerLever()
    {
        _isActive = !_isActive;
        if (_isActive)
        {
            for (int i = 0; i < _objToActivate.Length; i++) 
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f,0.0f,90.0f);
        }
        else
        {
            for (int i = 0; i < _objToActivate.Length; i++) 
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f,0.0f,-90.0f);
        }
        
    }
}
