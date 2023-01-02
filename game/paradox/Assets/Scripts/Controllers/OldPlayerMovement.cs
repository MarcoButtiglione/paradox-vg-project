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
    private bool _dash;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    
    private DynamicUIController _dynamicUIController;



    private void Awake(){
        _animator = gameObject.GetComponent<Animator>();
        _dynamicUIController = GameObject.Find("Dynamic").GetComponent<DynamicUIController>();

    }
    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.State is not (GameState.OldPlayerTurn or GameState.SecondPart)) return;
        
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _animator.SetBool(IsMoving, Math.Abs(_horizontalMove) > 0.01f);


        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            _jump = false;
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            _dash = true;
        }
        if (Input.GetButtonUp("Dash"))
        {
            _dash = false;
        }
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
}
