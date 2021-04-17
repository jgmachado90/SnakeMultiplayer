using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEater
{
    private readonly SnakeSettings _snakeSettings;

    public SnakeEater(SnakeSettings snakeSettings)
    {
        _snakeSettings = snakeSettings;
    }

    public GridCell GetFoodGridCellInTheLastTailPosition()
    {
        SnakePart currentTail = _snakeSettings.CurrentHead.Prev;
        if (currentTail.hasFood)
        {
            return currentTail.currentGridCell;
        }
        return null;
    }

    public float GetSnakeCurrentSpeed()
    {
        float speedDebuff = _snakeSettings.SpeedDebuff.Value * GetLoadedCount();
        float speedBuff = _snakeSettings.SpeedBuff.Value;
        float finalSpeed = _snakeSettings.MovementsPerSecond.Value + speedDebuff - speedBuff;
        return finalSpeed > 0 ? finalSpeed : _snakeSettings.SnakeMaxSpeed.Value;
    }

    private int GetLoadedCount()
    {
        int count = 0;
        foreach(SnakePart snakePart in _snakeSettings.Snake)
        {
            if (snakePart.hasFood)
            {
                count++;
            }
        }
        return count;
    }

    public void AddGrowCoordinate(GridCellCoordinates coordinates, Entity collector)
    {
        SnakePart snakePart = (SnakePart)collector;
        snakePart.hasFood = true;
        collector.transform.localScale = new Vector3(_snakeSettings.FeedScale.Value, _snakeSettings.FeedScale.Value, _snakeSettings.FeedScale.Value);
    }



}

