using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    private Animator _animator;
    private float _horizontalMove;
    [SerializeField] private float runSpeed = 40f;
    private bool _jump;
    private bool _holdJump;

    private bool _crouch;
    private List<TypeOfInputs> _inputs;
    private List<Vector3> _positionsYoungP;
    private List<bool> _youngWasGrounded;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private DynamicUIController _dynamicUIController;

    private PlayerInputActions _actions;

    private void Awake()
    {
        _actions = new PlayerInputActions();
    }
    

    private void Start(){
        _dynamicUIController = GameObject.Find("Canvases").GetComponentInChildren<DynamicUIController>();
        _inputs = new List<TypeOfInputs>();
        _animator = gameObject.GetComponent<Animator>();
        _positionsYoungP = new List<Vector3>();
        _youngWasGrounded = new List<bool>();
    }

    private void OnEnable()
    {
        _actions.YoungPlayer.Enable();
        _actions.YoungPlayer.Jump.performed += JumpPerformed;
        _actions.YoungPlayer.Jump.canceled += JumpCanceled;
    }

    private void OnDisable()
    {
        _actions.YoungPlayer.Disable();
        _actions.YoungPlayer.Jump.performed -= JumpPerformed;
        _actions.YoungPlayer.Jump.canceled -= JumpCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        
        var x = _actions.YoungPlayer.Move.ReadValue<Vector2>().x;
        _horizontalMove = x * runSpeed;
        _animator.SetFloat(Speed,Math.Abs(_horizontalMove));
        
        /*
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            _holdJump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            _holdJump = false;
        }
        */

        //UI TRIGGER UP DOWN
        if (Math.Abs(_horizontalMove) > 0 || _jump)
        {
            _dynamicUIController.SetMoving(true);
        }
        else
        {
            _dynamicUIController.SetMoving(false);
        }
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        _positionsYoungP.Insert(_positionsYoungP.Count, transform.position);
        _youngWasGrounded.Insert(_youngWasGrounded.Count,controller.GetGrounded());
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump,_holdJump);
        _inputs.Insert(_inputs.Count, new TypeOfInputs(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump,_holdJump)); 
        _jump = false;
    }
    

    public List<TypeOfInputs> GetListInputs(){
        return _inputs;
    }

    public List<Vector3> GetPosYoung(){
        return _positionsYoungP;
    }
    public List<bool> GetGroundedYoung(){
        return _youngWasGrounded;
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        _jump = true; 
        _holdJump = true;
    }
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _holdJump = false;
    }
}
