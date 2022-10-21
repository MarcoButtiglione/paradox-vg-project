using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheckEndLevel : MonoBehaviour
{
    //public static event Action OnPlayerDeath;
    private Rigidbody2D _rigidbody;
    private SceneController father;

private void Awake()
    {  
         _rigidbody = GetComponent<Rigidbody2D>();
    }
   private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Young")){
            father.StartRewind();
        }
        if(col.CompareTag("Ghost")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void setFather(SceneController father){
        this.father = father;
    }
}
