using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Input : ISnakeInput
{
    public Direction LookingDirection { get; set; }

    //TODO CHANGE ASAP
    public void ReadInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _ = ((int)LookingDirection - 1) < 0 ? MovingDirection = (Direction)3 : MovingDirection = LookingDirection - 1;


        if (Input.GetKeyDown(KeyCode.RightArrow))
            _ = ((int)LookingDirection + 1) > 3 ? MovingDirection = 0 : MovingDirection = LookingDirection + 1;

    }

    public Direction MovingDirection { get; private set; }
}
