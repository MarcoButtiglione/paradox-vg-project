using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young")||col.CompareTag("Old"))  
        {                                                                                                                
            col.gameObject.transform.SetParent(transform);                                                               
        }                                                                                                                
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Young")||col.CompareTag("Old"))    
        {                                                                                                                  
            col.gameObject.transform.SetParent(null);                                                                      
        }                                                                                                                  
    }
}
