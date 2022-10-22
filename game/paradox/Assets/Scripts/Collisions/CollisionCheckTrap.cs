using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionCheckTrap : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Young"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(col.gameObject.CompareTag("Old") || col.gameObject.CompareTag("Ghost"))
        {
            OnPlayerDeath?.Invoke();
        }
    }
    
}

