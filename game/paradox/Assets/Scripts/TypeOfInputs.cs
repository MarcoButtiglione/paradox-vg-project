using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TypeOfInputs
{

    private float horizontal;
    private bool jump;
    private bool crouch;

    public TypeOfInputs(float hor, bool c, bool j)
    {
        this.horizontal = hor;
        this.crouch = c;
        this.jump = j;
    }

    public float getHorizontal()
    {
        return horizontal;
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
