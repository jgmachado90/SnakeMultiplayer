using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : ISnakeInput
{
    //TODO CHANGE ALL THIS TRASH
    public void ReadInput()
    {

            if (Input.GetKeyDown(KeyCode.A))
        {
            int nextDirectionIndex = (int) Direction;
            if (nextDirectionIndex - 1 < 0)
                nextDirectionIndex = 4;
            Direction = (Direction)(nextDirectionIndex-1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            int nextDirectionIndex = (int)Direction;
            if (nextDirectionIndex + 1 > 3)
                nextDirectionIndex = -1;
            Direction = (Direction)(int)nextDirectionIndex + 1;
        }
    }

    public Direction Direction { get; private set; }
}
