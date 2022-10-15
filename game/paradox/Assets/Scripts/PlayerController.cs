using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 1f;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(transform.up*_jumpForce, ForceMode2D.Impulse);
        }
    }
}
