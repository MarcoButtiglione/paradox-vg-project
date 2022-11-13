using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivatorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _objToActivate;
    
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.StartingYoungTurn)
        {
            for (int i = 0; i < _objToActivate.Length; i++) 
            {
                if (_objToActivate[i].GetComponent<MovingPlatformController>())
                {
                    _objToActivate[i].GetComponent<MovingPlatformController>().SetActivated();
                }
                else if (_objToActivate[i].GetComponent<LaserController>())
                {
                    _objToActivate[i].GetComponent<LaserController>().StartPeriodic();
                }
                else
                {
                    _objToActivate[i].SetActive(true);
                }
               
            }
        }
        if (state == GameState.StartingOldTurn)
        {
            for (int i = 0; i < _objToActivate.Length; i++) 
            {
                if (_objToActivate[i].GetComponent<MovingPlatformController>())
                {
                    _objToActivate[i].GetComponent<MovingPlatformController>().SetDeactivated();
                }
                else if (_objToActivate[i].GetComponent<LaserController>())
                {
                    _objToActivate[i].GetComponent<LaserController>().StartFixed();
                }
                else
                {
                    _objToActivate[i].SetActive(false);
                }
               
            }
        }
    }
}
