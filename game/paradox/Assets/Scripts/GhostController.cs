using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public static event Action OnPlayerDeath;
    public CharacterController2D controller;
    private int index = 0;
    private TimeBody father;
    private bool rewind = false;



    void FixedUpdate()
    {

        if (rewind)
        {
            if (index < father.getInputs().Count)
            {

                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(father.getPositions()[index].x, father.getPositions()[index].y)) > 0.5)
                {
                    stopCycle();
                }

                controller.Move(father.getInputs()[index].getHorizontal() * Time.fixedDeltaTime, father.getInputs()[index].getCrouch(), father.getInputs()[index].getJump());
                index++;
            }
            else
            {
                stopCycle();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Old")){
            this.gameObject.SetActive(false);
            Debug.Log("Detected");
            stopCycle();
        }
    }

    public void setFather(TimeBody father)
    {
        this.father = father;
    }
    public void setRewind()
    {
        if (!rewind)
        {
            this.gameObject.SetActive(true);
            transform.position = father.getPositions()[0];

        }
        rewind = true;

    }
    
    public void stopCycle()
    {
        father.StopRewind();
        index = 0;
        rewind = false;
        OnPlayerDeath?.Invoke();
    }

}
