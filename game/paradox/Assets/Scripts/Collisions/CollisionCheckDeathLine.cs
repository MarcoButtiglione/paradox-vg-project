using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheckDeathLine : MonoBehaviour
{
    
    private SceneController father;
    public static event Action OnPlayerDeath;


   private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Young")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(col.CompareTag("Old"))
        {
            father.RestartRewind();
        }
    }
    public void setFather(SceneController father){
        this.father = father;
    }
}
