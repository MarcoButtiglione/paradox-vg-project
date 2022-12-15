using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivatorManager : MonoBehaviour
{
    //[SerializeField] private GameObject[] _objToActivate;
    private GameObject _activable;
    private GameObject _activators;
    
    
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        _activable = GameObject.Find("Activable");
        _activators = GameObject.Find("Activators");
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
            
            for(int i = 0; i < _activable.transform.childCount; i++)
            {
                GameObject child = _activable.transform.GetChild(i).gameObject;
            }
            
            
            for(int i = 0; i < _activators.transform.childCount; i++)
            {
                GameObject child = _activators.transform.GetChild(i).gameObject;
            }
            
            
            /*
            for (int i = 0; i < _objToActivate.Length; i++) 
            {
                if (_objToActivate[i].GetComponent<MovingPlatformController>())
                {
                    _objToActivate[i].GetComponent<MovingPlatformController>().SetActivated();
                }
                else if (_objToActivate[i].GetComponent<MovingLaserController>())
                {
                    _objToActivate[i].GetComponent<MovingLaserController>().SetActivated();
                }
                else if (_objToActivate[i].GetComponent<RotatingLaserController>())
                {
                    _objToActivate[i].GetComponent<RotatingLaserController>().SetActivated();
                }
                else
                {
                    _objToActivate[i].SetActive(true);
                }
               
            }
            */
        }
        if (state == GameState.StartingOldTurn)
        {
            /*
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
                else if (_objToActivate[i].GetComponent<MovingLaserController>())
                {
                    _objToActivate[i].GetComponent<MovingLaserController>().SetActivated();
                }
                else if (_objToActivate[i].GetComponent<RotatingLaserController>())
                {
                    _objToActivate[i].GetComponent<RotatingLaserController>().SetActivated();
                }
                else
                {
                    _objToActivate[i].SetActive(false);
                }
               
            }
            */
        }

        if (state == GameState.YoungPlayerTurn)
        {
            /*
            for (int i = 0; i < _objToActivate.Length; i++)
            {
                if (_objToActivate[i].GetComponent<LaserController>())
                {
                    _objToActivate[i].GetComponent<LaserController>().StartPeriodic();
                }
            }
            */
        }
    }
}
