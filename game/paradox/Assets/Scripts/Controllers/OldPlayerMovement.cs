using System;
using UnityEngine;


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
    private bool _jet ;
    private bool _dash;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsUsingJet = Animator.StringToHash("IsUsingJet");
    private static readonly int IsDashing = Animator.StringToHash("IsDashing");


    private void Awake(){
        _animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.State is not (GameState.OldPlayerTurn or GameState.SecondPart)) return;
        
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _animator.SetBool(IsMoving, Math.Abs(_horizontalMove) > 0.01f);


        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool(IsUsingJet,true);
            _jump = true;
            _jet = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            _animator.SetBool(IsUsingJet,false);
            _jet = false;
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            _animator.SetBool(IsDashing,true);
            _dash = true;
        }
        if (Input.GetButtonUp("Dash"))
        {
            _dash = false;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump, _jet, _dash);
        _jump = false;
        _animator.SetBool(IsDashing, false);
    }
}
