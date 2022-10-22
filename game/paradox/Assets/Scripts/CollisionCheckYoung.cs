using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckYoung : MonoBehaviour
{
    //public static event Action OnPlayerDeath;
    private Rigidbody2D _rigidbody;
    

private void Awake()
    {  
         _rigidbody = GetComponent<Rigidbody2D>();
    }
   
}
