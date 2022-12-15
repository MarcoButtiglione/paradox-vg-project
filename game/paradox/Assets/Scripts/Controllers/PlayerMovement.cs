using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    private Animator _animator;
    private float horizontalMove = 0f;
    [SerializeField] private float runSpeed = 40f;
    private bool jump = false;
    private bool crouch = false;
    private List<TypeOfInputs> inputs;
    

    void Start(){
        inputs = new List<TypeOfInputs>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.State == GameState.YoungPlayerTurn){
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _animator.SetFloat("Speed",Math.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        inputs.Insert(inputs.Count, new TypeOfInputs(horizontalMove * Time.fixedDeltaTime, crouch, jump));
        jump = false;
    }

    public float getHorizontal()
    {
        return horizontalMove * Time.fixedDeltaTime;
    }

    public bool getJump()
    {
        return jump;
    }

    public bool getCrouch()
    {
        return crouch;
    }

    public List<TypeOfInputs> getListInputs(){
        return inputs;
    }
}
