using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : ISnakeInput
{
    public void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Direction = Direction.Up;
        if (Input.GetKeyDown(KeyCode.A))
            Direction = Direction.Left;
        if (Input.GetKeyDown(KeyCode.S))
            Direction = Direction.Down;
        if (Input.GetKeyDown(KeyCode.D))
            Direction = Direction.Right;
    }

    public Direction Direction { get; private set; }
}
