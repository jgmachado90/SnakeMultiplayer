using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEater
{
    private readonly List<Entity> _snakeParts;
    private readonly SnakeSettings _snakeSettings;

    public SnakeEater(List<Entity> snakeParts, SnakeSettings snakeSettings)
    {
        _snakeParts = snakeParts;
        _snakeSettings = snakeSettings;
    }

    public GridCell GetFoodGridCellInTheLastTailPosition()
    {
        SnakePart lastSnakePart =(SnakePart)_snakeSettings.CurrentHead.Prox;

        if (lastSnakePart.hasFood)
        {
            return lastSnakePart.currentGridCell;
        }
        return null;
    }

    public float GetSnakeCurrentSpeed()
    {
        float speedDebuff = 0; // _snakeSettings.SpeedDebuff.Value * _growPositions.Count;
        float speedBuff = _snakeSettings.SpeedBuff.Value;
        float finalSpeed = _snakeSettings.MovementsPerSecond.Value + speedDebuff - speedBuff;
        return finalSpeed > 0 ? finalSpeed : _snakeSettings.SnakeMaxSpeed.Value;
    }


    public void AddGrowCoordinate(GridCellCoordinates coordinates, Entity collector)
    {
        SnakePart snakePart = (SnakePart)collector;
        snakePart.hasFood = true;
        collector.transform.localScale = new Vector3(_snakeSettings.FeedScale.Value, _snakeSettings.FeedScale.Value, _snakeSettings.FeedScale.Value);
    }



}

