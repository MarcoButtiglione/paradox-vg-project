
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public CharacterController2D controller;
    private SceneController father;
    
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Old")){
            father.RepeatRewind();
        }
    }

    public void setFather(SceneController father)
    {
        this.father = father;
    }


}
