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
    private bool holdJump = false;
    private bool crouch = false;
    private List<TypeOfInputs> inputs;
    private List<Vector3> positions_young_p;
    

    void Start(){
        inputs = new List<TypeOfInputs>();
        _animator = gameObject.GetComponent<Animator>();
        positions_young_p = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.YoungPlayerTurn) return;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _animator.SetFloat("Speed",Math.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            holdJump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            holdJump = false;
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

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump,holdJump);
        inputs.Insert(inputs.Count, new TypeOfInputs(horizontalMove * Time.fixedDeltaTime, crouch, jump,holdJump));
        positions_young_p.Insert(positions_young_p.Count, transform.position);
        jump = false;
        
        //Debug.Log("DeltaTimeMovement: " + Time.fixedDeltaTime);
        //Temporary, trying to synchronize this and RewindManager
        //new WaitForEndOfFrame();
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

    public List<Vector3> getPosYoung(){
        return positions_young_p;
    }
}
