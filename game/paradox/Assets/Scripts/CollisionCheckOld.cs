using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckOld : MonoBehaviour
{

private Rigidbody2D _rigidbody;

private void Awake()
    {  
         _rigidbody = GetComponent<Rigidbody2D>();
    }
   private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Ghost")){
            this.gameObject.SetActive(false);
            Debug.Log("Detected");
        }
    }
}
