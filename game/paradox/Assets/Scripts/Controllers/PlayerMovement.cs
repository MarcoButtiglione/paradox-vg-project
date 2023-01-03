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

    private PlayerInputactions _controls;

    private void Awake()
    {
        _controls = new PlayerInputactions();
    }

    private void OnEnable()
    {
        _controls.YoungPlayer.Enable();
        //_controls.YoungPlayer.Jump.performed += SetJump();
        _controls.YoungPlayer.Jump.performed += SetJumpStarted;
        _controls.YoungPlayer.Jump.canceled += SetJumpCancelled;

    }

   


    private void OnDisable()
    {
        _controls.YoungPlayer.Disable();
    }

    private void Start(){
        _dynamicUIController = GameObject.Find("Canvases").GetComponentInChildren<DynamicUIController>();
        _inputs = new List<TypeOfInputs>();
        _animator = gameObject.GetComponent<Animator>();
        _positionsYoungP = new List<Vector3>();
        _youngWasGrounded = new List<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        /*
        var j = Keyboard.current.anyKey.isPressed;
        var gamepad = Gamepad.current;
        if (gamepad != null)
            Debug.Log("CONTROLLER");
        if(j)
            Debug.Log(j);
        */
        
        _horizontalMove = _controls.YoungPlayer.Movement.ReadValue<float>() * runSpeed;
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

    public void SetJump(InputAction.CallbackContext context)
    {
        //Debug.Log("jump "+context);
    }

    private void SetJumpStarted(InputAction.CallbackContext context)
    {
        _jump = true;
        _holdJump = true;
    }

    private void SetJumpCancelled(InputAction.CallbackContext context)
    {
        _holdJump = false;
    }
    

}
