using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckYoung : MonoBehaviour
{
    //public static event Action OnPlayerDeath;
    private Rigidbody2D _rigidbody;
    private SceneController father;

private void Awake()
    {  
         _rigidbody = GetComponent<Rigidbody2D>();
    }
   private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("EndLevel")){
            father.StartRewind();
        }
    }
    public void setFather(SceneController father){
        this.father = father;
    }
}
