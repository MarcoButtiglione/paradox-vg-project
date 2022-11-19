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
    private float horizontalMove = 0f;
    [SerializeField] private float runSpeed = 40f;
    private bool jump = false;
    private bool crouch = false;
    private bool jet = false;
    private bool dash = false;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jet = true;
        }else if (Input.GetButtonUp("Jump"))
        {
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, jet, dash);
        jump = false;
        dash = false;
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
