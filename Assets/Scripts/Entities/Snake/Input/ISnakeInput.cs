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
    Direction MovingDirection { get; }
    Direction LookingDirection { get; set; }

}
