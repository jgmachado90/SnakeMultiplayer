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

    KeyCode LeftKey { get; set; }
    KeyCode RightKey { get; set; }

    Direction MovingDirection { get; set; }
    Direction LookingDirection { get; set; }

}
