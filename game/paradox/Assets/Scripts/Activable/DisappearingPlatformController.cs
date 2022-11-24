using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO dev intermittent
public class DisappearingPlatformController : MonoBehaviour
{
    [SerializeField] private DisappearingFunctioning _function;
    [Tooltip("Set the period if function is 'Intermittent'. Set function 'Intermittent'.")]
    [HideInInspector][Range(0.0f, 10.0f)] public float _intermittentPeriod=0f;
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private DisappearingState _initYoungState;
    [SerializeField] private DisappearingState _initOldStateOFF;

    private bool _isActive;
    
    
    //-------------------------------
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
            InitYoung();
        }
        if (state == GameState.StartingOldTurn)
        {
            InitOld();
        }
    }
    private void InitYoung()
    {
        if (_initYoungState == DisappearingState.Active)
        {
            StopAllCoroutines();
            _isActive = true;
            if (_function == DisappearingFunctioning.Fixed)
            {
                gameObject.SetActive(true);
            }
            /*
            else if (_function == DisappearingFunctioning.Intermittent)
            {
                gameObject.SetActive(true);
                StartCoroutine(Intermittent());
            } */
        }
        else if (_initYoungState==DisappearingState.Inactive)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    }
    private void InitOld()
    {
        StopAllCoroutines();
        if (_initOldStateOFF == DisappearingState.Active)
        {
            _isActive = true;
            if (_function == DisappearingFunctioning.Fixed)
            {
                gameObject.SetActive(true);
            }
            /*
            else if (_function == DisappearingFunctioning.Intermittent)
            {
                StopCoroutine(Intermittent());
            } 
            */
        }
        else if (_initOldStateOFF==DisappearingState.Inactive)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    }
    //-------------------------------

    /*
    private IEnumerator Intermittent()
    {
        while (_isActive)
        {
            Debug.Log("CORUTINE");
            gameObject.SetActive(false);
            yield return new WaitForSeconds(_intermittentPeriod/2);
            gameObject.SetActive(true);
            yield return new WaitForSeconds(_intermittentPeriod/2);
        }
        
    }
    */
    public void SwitchState()
    {
        if (_isActive)
        {
            _isActive = false;
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
        else
        {
            _isActive = true;
            if (_function == DisappearingFunctioning.Fixed)
            {
                gameObject.SetActive(true);
            }
            /*
            else if (_function == DisappearingFunctioning.Intermittent)
            {
                gameObject.SetActive(true);
                StartCoroutine(Intermittent());
            }*/
        }
        
    }
}


[System.Serializable]
public enum DisappearingState
{
    Inactive,
    Active
}
[System.Serializable]
public enum DisappearingFunctioning
{
    Fixed,
    //Intermittent
}

