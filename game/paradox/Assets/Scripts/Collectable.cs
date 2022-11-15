using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private CollectableManager _cm;

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("CollectableManager").GetComponent<CollectableManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young"))
        {
            _cm.AddCollectableCount(col.tag);
            this.gameObject.SetActive(false); 
        }
        else if (col.CompareTag("Ghost"))
        {
            _cm.AddCollectableCount(col.tag);
            this.gameObject.SetActive(false); 
        }
    }
}
