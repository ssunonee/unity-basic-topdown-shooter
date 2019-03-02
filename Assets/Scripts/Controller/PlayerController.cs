using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IController
{

    public bool GetMoveInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return true;
        }
        return false;
    }

    public bool GetRotateLeftInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }
        return false;
    }

    public bool GetRotateRightInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }
        return false;
    }

    public bool ShouldShoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
}
