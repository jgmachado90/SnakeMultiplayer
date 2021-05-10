using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : ISnakeInput
{
    public Direction LookingDirection { get; set; }

    public KeyCode LeftKey { get; set; }
    public KeyCode RightKey { get; set; }

    //TODO CHANGE ASAP
    public void ReadInput()
    {
        if (Input.GetKeyDown(LeftKey))
            _ = ((int)LookingDirection - 1) < 0 ? MovingDirection = (Direction)3 : MovingDirection = LookingDirection - 1;           


        if (Input.GetKeyDown(RightKey))
            _ = ((int)LookingDirection + 1) > 3 ? MovingDirection = 0 : MovingDirection = LookingDirection + 1;
    }

    public Direction MovingDirection { get;set; }
}
