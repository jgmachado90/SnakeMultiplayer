using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInput : ISnakeInput
{
    public Direction LookingDirection { get; set; }
    public void ReadInput()
    {
        MovingDirection = (Direction)Random.Range(0, 4);
    }

    public Direction MovingDirection { get; private set; }
}
