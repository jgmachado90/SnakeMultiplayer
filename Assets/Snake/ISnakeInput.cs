using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction 
{ 
    Right,
    Left,
    Up,
    Down
}
public interface ISnakeInput {
    void ReadInput();
    Direction Direction { get; }
}
