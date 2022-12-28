using System;
using System.Collections.Generic;
using UnityEngine;

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


    private void Start(){
        _inputs = new List<TypeOfInputs>();
        _animator = gameObject.GetComponent<Animator>();
        _positionsYoungP = new List<Vector3>();
        _youngWasGrounded = new List<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _animator.SetFloat(Speed,Math.Abs(_horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            _holdJump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            _holdJump = false;
        }
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump,_holdJump);
        _inputs.Insert(_inputs.Count, new TypeOfInputs(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump,_holdJump));
        _positionsYoungP.Insert(_positionsYoungP.Count, transform.position);
        _youngWasGrounded.Insert(_youngWasGrounded.Count,controller.GetGrounded());
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
}
