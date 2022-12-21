using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VALUE USED IN OLDPLAYERMOVEMENT TO USE IT

//JUMP FORCE = 600
//JET FORCE = 0.05
//DASH FORCE = 1500


public class OldPlayerMovement : MonoBehaviour
{
    public OldController2D controller;
    private Animator _animator;
    private float horizontalMove = 0f;
    [SerializeField] private float runSpeed = 40f;
    private bool jump = false;
    private bool crouch = false;
    private bool jet = false;
    private bool dash = false;


    void Awake(){
        _animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State!=GameState.StartingOldTurn
            &&GameManager.Instance.State!=GameState.StartingSecondPart
            &&GameManager.Instance.State!=GameState.StartingThirdPart
            &&GameManager.Instance.State!=GameState.StartingYoungTurn
           )
        {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        
            if (Math.Abs(horizontalMove)>0.01f)
            {
                _animator.SetBool("IsMoving",true);
            }
            else
            {
                _animator.SetBool("IsMoving",false);
            }
        
        
        
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("IsUsingJet",true);
            jump = true;
            jet = true;
        }else if (Input.GetButtonUp("Jump"))
        {
            _animator.SetBool("IsUsingJet",false);
            jet = false;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonUp("Dash"))
        {
            _animator.SetBool("IsDashing",true);
            dash = true;
        }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, jet, dash);
        jump = false;
        dash = false;
        _animator.SetBool("IsDashing",false);
    }

    public float getHorizontal()
    {
        return horizontalMove;
    }

    public bool getJump()
    {
        return jump;
    }

    public bool getCrouch()
    {
        return crouch;
    }
}
