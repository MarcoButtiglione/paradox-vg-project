using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheckEndLevel : MonoBehaviour
{
    private bool _doorIsActive = false;
    private CollectableManager _cm;

    [SerializeField]private GameObject _doorOn;
    [SerializeField]private GameObject _doorOnClosed;
    [SerializeField] private GameObject _doorOff;
    
    public void ActivateDoor()
    {
        _doorIsActive = true;
        _doorOn.SetActive(true);
        _doorOff.SetActive(false);
        _doorOnClosed.SetActive(false);
    }

    public void DisableDoor()
    {
        _doorIsActive = false;
        _doorOn.SetActive(false);
        _doorOnClosed.SetActive(false);
        _doorOff.SetActive(true);
    }

    private void Start()
    {
        _cm = GameObject.FindGameObjectWithTag("CollectableManager").GetComponent<CollectableManager>();
        if(_cm.GetNumberToCollect() == 0)
        {
            _doorIsActive = true;
            _doorOn.SetActive(true);
            _doorOnClosed.SetActive(false);
            _doorOff.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young"))
        {
            if (GameManager.Instance.IsTutorial())
            {
                GameManager.Instance.UpdateGameState(GameState.StartingThirdPart);
            }
            else if (_doorIsActive)
            {
                _doorIsActive = false;
                _doorOn.SetActive(false);
                _doorOff.SetActive(true);
                _doorOnClosed.SetActive(false);
                GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            }
        }
        else if (col.CompareTag("Ghost") && GameManager.Instance.State != GameState.NextLevel && _doorIsActive)
        {
            _doorIsActive = false;
            col.gameObject.SetActive(false);
            _doorOn.SetActive(false);
            _doorOff.SetActive(false);
            _doorOnClosed.SetActive(true);
            GameManager.Instance.UpdateGameState(GameState.LevelCompleted);
        }
    }

}
