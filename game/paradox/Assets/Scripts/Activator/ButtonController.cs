using System;
using System.Collections;
using System.Collections.Generic;
// It was ''Unity.VectorGraphics.Editor;'' but it made the game not buildable 
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    
    [SerializeField] private float _timer = 1.0f;
    [SerializeField] private GameObject[] _objToActivate;
    [Header("Initial state (Young/Old phase)")]
    [HideInInspector][SerializeField] private bool _initYoungState=false;
    [HideInInspector][SerializeField] private bool _initOldState=false;
    
    private bool _isActive=false;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private Sprite _spriteOn;
    private SpriteRenderer _spriteRenderer;
    private GameState _state;
    
    [SerializeField] private Image cooldownImage;
    private float _cooldownTime;
    private bool _isActiveCooldown;

    private GameObject _canvas;


    //-------------------------------
    private void Awake()
    {
        _spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        _canvas = gameObject.transform.GetChild(0).gameObject;
        cooldownImage.fillAmount = 0;
        _canvas.transform.eulerAngles = Vector3.zero;
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
        _state = state;
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
        _isActiveCooldown = false;
        StopAllCoroutines();
        if (_initYoungState)
        {
            _spriteRenderer.sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            _spriteRenderer.sprite = _spriteOff;
            _isActive = false;
        }
        
    }
    private void InitOld()
    {
        _isActiveCooldown = false;
        StopAllCoroutines();
        if (_initOldState)
        {
            _spriteRenderer.sprite = _spriteOn;
            _isActive = true;
        }
        else
        {
            _spriteRenderer.sprite = _spriteOff;
            _isActive = false;
        }
    }
    //-------------------------------
    private void Update()
    {
        if (_isActiveCooldown)
        {
            _cooldownTime -= Time.deltaTime;
            cooldownImage.fillAmount = _cooldownTime / _timer;
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }
    }

    private void SetActive()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOn;
        _spriteRenderer.sprite = _spriteOn;
        _isActive = true;
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<ActivableController>())
            {
                _objToActivate[i].GetComponent<ActivableController>().SwitchState();
            }
        }
    }
    private void SetInactive()
    {
        _spriteRenderer.sprite = _spriteOff;
        //gameObject.GetComponent<SpriteRenderer>().sprite = _spriteOff;
        
        _isActive = false;
        for (int i = 0; i < _objToActivate.Length; i++) 
        {
            if (_objToActivate[i].GetComponent<ActivableController>())
            {
                _objToActivate[i].GetComponent<ActivableController>().SwitchState();
            }
        }
    }
    

    
    public void TriggerButtom()
    {
        if (GameManager.Instance.IsPlayablePhase())
        {
            StartCoroutine(TriggerButtomCor());
        }
    }
    
    IEnumerator TriggerButtomCor()
    {
        if (!_isActive)
        {
            if (_state == GameState.YoungPlayerTurn)
            {
                FindObjectOfType<AudioManager>().Play("Error");
            }
            else
            {
                _isActiveCooldown = true;
                _cooldownTime = _timer;
                SetActive();
                //Play the click sound-----
                FindObjectOfType<AudioManager>().Play("Click");
                //------------------------
                yield return new WaitForSeconds(_timer);
                SetInactive();
                _isActiveCooldown = false;

            }
        }
    }
}
