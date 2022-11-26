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

    [Header("Sprites")]
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    
    private bool _isActive;
    
    
    //-------------------------------
    private void Awake()
    {
        _spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        _collider2D = gameObject.GetComponent<Collider2D>();
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
        StopAllCoroutines();
        if (_initYoungState == DisappearingState.Active)
        {
            SetActive();
        }
        else if (_initYoungState==DisappearingState.Inactive)
        {
            SetInactive();
        }
    }
    private void InitOld()
    {
        StopAllCoroutines();
        if (_initOldStateOFF == DisappearingState.Active)
        {
            SetActive();
        }
        else if (_initOldStateOFF==DisappearingState.Inactive)
        {
            SetInactive();
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
            SetInactive();
            StopAllCoroutines();
        }
        else
        {
            SetActive();
        }
        
    }


    private void SetActive()
    {
        _spriteRenderer.sprite = _spriteOn;
        _isActive = true;
        if (_function == DisappearingFunctioning.Fixed)
        {
            _collider2D.enabled = true;            
        }
    }

    private void SetInactive()
    {
        _spriteRenderer.sprite = _spriteOff;
        _isActive = false;
        _collider2D.enabled = false;
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

