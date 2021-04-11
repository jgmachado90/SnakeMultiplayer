using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInput : ISnakeInput
{
   public void ReadInput()
    {
        Direction = (Direction)Random.Range(0, 4);
    }

    public Direction Direction { get; private set; }
}
