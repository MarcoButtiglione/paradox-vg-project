using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField]private float _plateMovement = 0.05f;
    [SerializeField] private GameObject[] _objToActivate;
    private Vector3 _originalPos;
    private bool _moveBack;
    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Old"))
        {
            if (Math.Abs(transform.position.y-_originalPos.y)< _plateMovement)
            {
                transform.Translate(0, -0.01f, 0);
            }
            else
            {
                if (!_isActive)
                {
                    _isActive = true;
                    for (int i = 0; i < _objToActivate.Length; i++) 
                    {
                        _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
                    }
                }   
            }
            _moveBack = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Old"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Old"))
        {
            _moveBack = true;
            collision.transform.parent = null;
        }    
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveBack)
        {
            if (transform.position.y < _originalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                if (_isActive)
                {
                    for (int i = 0; i < _objToActivate.Length; i++) 
                    {
                        _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
                    }
                    _isActive = false;
                }
                
                _moveBack = false;
            }
        }
    }
}
