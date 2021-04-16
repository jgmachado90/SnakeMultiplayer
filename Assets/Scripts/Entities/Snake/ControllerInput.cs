using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : ISnakeInput
{
    private Direction _lookingDirection;
    public Direction LookingDirection { get { return _lookingDirection; } set { _lookingDirection = value; } }

    //TODO CHANGE ASAP
    public void ReadInput()
    {

        if (Input.GetKeyDown(KeyCode.A))
            _ = ((int)LookingDirection - 1) < 0 ? MovingDirection = (Direction)3 : MovingDirection = LookingDirection - 1;           


        if (Input.GetKeyDown(KeyCode.D))
            _ = ((int)LookingDirection + 1) > 3 ? MovingDirection = 0 : MovingDirection = LookingDirection + 1;

    }

    public Direction MovingDirection { get; private set; }
}
