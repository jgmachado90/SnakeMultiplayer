using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/InGame", fileName = "SnakeInGameSettings")]
public class SnakeInGameSettings : ScriptableObject
{
    [SerializeField] private List<Snake> _snakes;

    
   public void AddNewSnake(Snake snake)
    {
        _snakes.Add(snake);
    }

}
