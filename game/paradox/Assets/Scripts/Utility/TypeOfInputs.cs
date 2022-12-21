using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TypeOfInputs
{

    private float horizontal;
    private bool jump;
    private bool holdJump;
    private bool crouch;

    public TypeOfInputs(float hor, bool c, bool j,bool hJ)
    {
        this.horizontal = hor;
        this.crouch = c;
        this.jump = j;
        this.holdJump = hJ;
    }

    public float getHorizontal()
    {
        return horizontal;
    }

    public bool getJump()
    {
        return jump;
    }
    public bool getHoldJump()
    {
        return holdJump;
    }
    public bool getCrouch()
    {
        return crouch;
    }


}
