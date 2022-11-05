using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private bool _isActive=false;
    [SerializeField] private float _timer = 1.0f;
    [SerializeField] private GameObject[] _objToActivate;

    
    public void TriggerButtom()
    {
        StartCoroutine(TriggerButtomCor());
    }
    
    
    IEnumerator TriggerButtomCor()
    {
        if (!_isActive)
        {
            //Play the click sound-----
            AudioManager a = FindObjectOfType<AudioManager>();
            if(a)
                a.Play("Click");
            //-------------------------
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
            yield return new WaitForSeconds(_timer);
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
        
    }
}
