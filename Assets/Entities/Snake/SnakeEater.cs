﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEater
{
    private readonly List<Entity> _snakeTail;
    private readonly Entity _snakeHead;
    private readonly SnakeSettings _snakeSettings;

    private List<GridCellCoordinates> _growPositions = new List<GridCellCoordinates>();

    public SnakeEater(List<Entity> tail, SnakeSettings snakeSettings)
    {
        _snakeTail = tail;
        _snakeHead = _snakeTail[0];
        _snakeSettings = snakeSettings;
    }

    public GridCell GetFoodGridCellInTheLastTailPosition()
    {
        Entity lastSnakeTail = _snakeTail[_snakeTail.Count - 1];

        foreach (GridCellCoordinates growPositions in _growPositions)
        {
            if (lastSnakeTail.currentGridCell.coordinate.x == growPositions.x &&
                 lastSnakeTail.currentGridCell.coordinate.y == growPositions.y)
            {
                return lastSnakeTail.currentGridCell;
            }
        }
        return null;
    }


    public void AddGrowCoordinate(GridCellCoordinates coordinates)
    {
        _growPositions.Add(coordinates);
    }

    public void RemoveGrowCoordinate(GridCellCoordinates coordinates)
    {
        _growPositions.Remove(coordinates);
    }


}
