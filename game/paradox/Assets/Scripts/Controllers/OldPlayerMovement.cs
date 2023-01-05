using System;
using UnityEngine;
using UnityEngine.InputSystem;



//JUMP FORCE = 600
//JET FORCE = 0.05
//DASH FORCE = 1500


public class OldPlayerMovement : MonoBehaviour
{
    public OldController2D controller;
    private Animator _animator;
    private float _horizontalMove;
    [SerializeField] private float runSpeed = 40f;
    private bool _jump ;
    private bool _dash;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    
    private DynamicUIController _dynamicUIController;

    private PlayerInputActions _actions;


    private void Awake(){
        _animator = gameObject.GetComponent<Animator>();
        _actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _actions.OldPlayer.Enable();
        _actions.OldPlayer.Jump.performed += JumpPerformed;
        _actions.OldPlayer.Jump.canceled += JumpCanceled;
        _actions.OldPlayer.Dash.performed += DashPerformed;
        _actions.OldPlayer.Dash.canceled += DashCanceled;
    }
    private void OnDisable()
    {
        _actions.OldPlayer.Disable();
        _actions.OldPlayer.Jump.performed -= JumpPerformed;
        _actions.OldPlayer.Jump.canceled -= JumpCanceled;
        _actions.OldPlayer.Dash.performed -= DashPerformed;
        _actions.OldPlayer.Dash.canceled -= DashCanceled;
    }
    private void Start()
    {
        _dynamicUIController = GameObject.Find("Canvases").GetComponentInChildren<DynamicUIController>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.State is not (GameState.OldPlayerTurn or GameState.SecondPart)) return;
        
        var x = _actions.OldPlayer.Move.ReadValue<Vector2>().x;

        _horizontalMove = x* runSpeed;
        _animator.SetBool(IsMoving, Math.Abs(_horizontalMove) > 0.01f);
        
        //UI TRIGGER UP DOWN
        if (Math.Abs(_horizontalMove) > 0 || _jump||_dash)
        {
            _dynamicUIController.SetMoving(true);
        }
        else
        {
            _dynamicUIController.SetMoving(false);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump, _dash);
    }
    private void JumpPerformed(InputAction.CallbackContext context)
    {
        _jump = true; 
    }
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _jump = false;
    }
    private void DashPerformed(InputAction.CallbackContext context)
    {
        _dash = true; 
    }
    private void DashCanceled(InputAction.CallbackContext context)
    {
        _dash = false; 
    }
}
