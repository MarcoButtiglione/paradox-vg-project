using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Young_Player"||col.gameObject.name == "Old_Player"||col.gameObject.name == "Ghost")  
        {                                                                                                                
            col.gameObject.transform.SetParent(transform);                                                               
        }                                                                                                                
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Young_Player"||col.gameObject.name == "Old_Player"||col.gameObject.name == "Ghost")    
        {                                                                                                                  
            col.gameObject.transform.SetParent(null);                                                                      
        }                                                                                                                  
    }
}
