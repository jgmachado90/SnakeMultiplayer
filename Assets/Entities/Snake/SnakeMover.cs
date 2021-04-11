using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover
{
    private readonly ISnakeInput _snakeInput;
    private readonly List<Entity> _snakeTail;
    private readonly Entity _snakeHead;

    public SnakeMover(ISnakeInput snakeInput, List<Entity> tail, SnakeSettings snakeSettings)
    {
        _snakeInput = snakeInput;
        _snakeTail = tail;
        _snakeHead = _snakeTail[0];
    }

    public void Tick()
    {
        if (_snakeHead.currentGridCell == null) return;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        int newX = _snakeHead.currentGridCell.coordinate.x;
        int newY = _snakeHead.currentGridCell.coordinate.y;

        if (_snakeInput.Direction == Direction.Up)
        {
            newY++;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.Direction == Direction.Down)
        {
            newY--;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.Direction == Direction.Left)
        {
            newX--;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.Direction == Direction.Right)
        {
            newX++;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //Transform headBeforeMovement = _head;
        //SetPosition(newX, newY);



        for(int i = _snakeTail.Count -1; i > 0; i--)
        {
            int x = _snakeTail[i - 1].currentGridCell.coordinate.x;
            int y = _snakeTail[i - 1].currentGridCell.coordinate.y;
            
            SetPosition(_snakeTail[i], x, y);
        }

        CheckNextCellEntities(newX, newY);
        SetPosition(_snakeHead, newX, newY);

    }

    private void CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = GridGenerator.instance.GetGridCellByCoordinate(x, y);
       
        if(nextCell == null)return;
        if (nextCell.entityOcupating == null) return;

        if(nextCell.entityOcupating is ICollectable)
        {
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snakeHead);
        }
    }

    private void SetPosition(Entity entity, int x, int y)
    {
        entity.currentGridCell = GridGenerator.instance.GetGridCellByCoordinate(x, y);
        entity.transform.position = new Vector3(x, y, 0);
    }
}
