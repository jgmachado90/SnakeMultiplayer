using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction 
{ 
    Right,
    Down,
    Left,
    Up
}
public interface ISnakeInput {
    void ReadInput();
    Direction Direction { get; }
}
