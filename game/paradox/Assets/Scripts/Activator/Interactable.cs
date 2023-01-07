using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{

    Animator range_Animator;

    private bool _isInRange;
    //NOT USED NOW
    //[SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;
    private PlayerInputactions _actions;
    private bool _oldInteraction;
    private bool _youngInteraction;
    
    private void Awake()
    {
        _actions = new PlayerInputactions();
        
    }

    private void OnEnable()
    {
        _actions.YoungPlayer.Enable();
        _actions.OldPlayer.Enable();
        _actions.YoungPlayer.Interact.performed += YoungInteractionPerformed;
        _actions.OldPlayer.Interact.performed += OldInteractionPerformed;
    }

    

    private void OnDisable()
    {
        _actions.YoungPlayer.Disable();
        _actions.OldPlayer.Disable();
        _actions.YoungPlayer.Interact.performed -= YoungInteractionPerformed;
        _actions.OldPlayer.Interact.performed -= OldInteractionPerformed;
    }

    // Start is called before the first frame update
    void Start()
    {
      range_Animator = gameObject.GetComponent<Animator>();
    }

    
    // Update is called once per frame
    /*
    void Update()
    {
        if (_isInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                interactAction.Invoke();
            }

        }
    }
    */
    void Update()
    {
        if (_oldInteraction||_youngInteraction)
        { 
            interactAction.Invoke();
            _oldInteraction = false;
            _youngInteraction = false;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Young"))
        {
            _isInRange = true;
        }
        if (col.gameObject.CompareTag("Old"))
        {
            _isInRange = true;
            range_Animator.SetBool("inRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Young"))
        {
            _isInRange = false;
        }
        if (col.gameObject.CompareTag("Old"))
        {
            _isInRange = false;
            range_Animator.SetBool("inRange", false);
        }
    }
    private void OldInteractionPerformed(InputAction.CallbackContext obj)
    {
        if (_isInRange)
        {
            _oldInteraction = true;
        }
    }

    private void YoungInteractionPerformed(InputAction.CallbackContext obj)
    {
        if (_isInRange)
        {
            _youngInteraction = true;
        }
    }
}
