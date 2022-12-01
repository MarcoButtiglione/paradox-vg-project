using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private CollectableManager _cm;
    private Vector3 initialPos;

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("CollectableManager").GetComponent<CollectableManager>();
        initialPos = this.gameObject.transform.position;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young"))
        {
            _cm.AddCollectableCount(col.tag);
            this.gameObject.transform.position = initialPos;
            this.gameObject.SetActive(false); 
        }
        else if (col.CompareTag("Ghost"))
        {
            _cm.AddCollectableCount(col.tag);
            this.gameObject.transform.position = initialPos;
            this.gameObject.SetActive(false); 
        }
    }
}
