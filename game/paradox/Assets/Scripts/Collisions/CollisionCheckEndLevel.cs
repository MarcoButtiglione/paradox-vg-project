using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheckEndLevel : MonoBehaviour
{
    private bool _doorIsActive = false;
    private CollectableManager _cm;
    
    public void ActivateDoor()
    {
        _doorIsActive = true;
    }

    public void DisableDoor()
    {
        _doorIsActive = false;
    }

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("CollectableManager").GetComponent<CollectableManager>();
        if(_cm.GetNumberToCollect() == 0)
        {
            Debug.Log("NUMBER TO COLLECT: " + _cm.GetNumberToCollect());
            _doorIsActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Door state is= " + _doorIsActive);
        if (col.CompareTag("Young"))
        {
            Debug.Log("Door is not active : door value is=" + _doorIsActive);
            if (GameManager.Instance.IsTutorial())
            {
                GameManager.Instance.UpdateGameState(GameState.StartingThirdPart);
            }
            else if (_doorIsActive)
            {
                _doorIsActive = false;
                GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            }
        }
        else if (col.CompareTag("Ghost") && GameManager.Instance.State != GameState.NextLevel && _doorIsActive)
        {
            _doorIsActive = false;
            GameManager.Instance.UpdateGameState(GameState.LevelCompleted);
        }
    }

}
