using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeEater
{
    private readonly SnakeSettings _snakeSettings;
    private readonly Snake _snake;
    public List<CollectableType> _nextTailParts = new List<CollectableType>();

    public SnakeEater(SnakeSettings snakeSettings, Snake snake)
    {
        _snakeSettings = snakeSettings;
        _snake = snake;
    }

    public bool HasFoodInTheLastPosition()
    {
        if(_snake.ThisSnake.Count == 0)
            return false;
        return _snake.ThisSnake.Last().HasFood;
        
    }

    public float GetSnakeCurrentSpeed()
    {
        float speedDebuff = _snakeSettings.SnakeMovementSettings.SpeedDebuff.Value * GetLoadedCount();
        float speedBuff = _snakeSettings.SnakeMovementSettings.SpeedBuff.Value;
        float finalSpeed = _snakeSettings.SnakeMovementSettings.MovementsPerSecond.Value + speedDebuff - speedBuff;
        return finalSpeed > 0 ? finalSpeed : _snakeSettings.SnakeMovementSettings.SnakeMaxSpeed.Value;
    }

    private int GetLoadedCount()
    {
        int count = 0;
        foreach(SnakeBlock snakePart in _snake.ThisSnake)
        {
            if (snakePart.HasFood)
            {
                count++;
            }
        }
        return count;
    }

    internal CollectableType NextTailPartsPop()
    {
        CollectableType nextTail = _nextTailParts.Last();
        _nextTailParts.Remove(nextTail);
        return nextTail;
    }
}

