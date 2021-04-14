using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover
{
    private readonly ISnakeInput _snakeInput;
    private readonly List<Entity> _snakeTail;
    private readonly Entity _snakeHead;
    private readonly GridInfo _gridInfo;

    private Direction lookingDirection;

    public SnakeMover(ISnakeInput snakeInput, List<Entity> tail, SnakeSettings snakeSettings, GridInfo gridInfo)
    {
        _snakeInput = snakeInput;
        _snakeTail = tail;
        _snakeHead = _snakeTail[0];
        _gridInfo = gridInfo;
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

        if (_snakeInput.Direction == Direction.Up && lookingDirection != Direction.Down)
        {
            newY++;
            lookingDirection = Direction.Up;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.Direction == Direction.Down && lookingDirection != Direction.Up)
        {
            newY--;
            lookingDirection = Direction.Down;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.Direction == Direction.Left && lookingDirection != Direction.Right)
        {
            newX--;
            lookingDirection = Direction.Left;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.Direction == Direction.Right && lookingDirection != Direction.Left)
        {
            lookingDirection = Direction.Right;
            newX++;
            _snakeHead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        MoveSnakeTail();
        MoveSnakeHead(newX, newY);

    }

    private void MoveSnakeHead(int newX, int newY)
    {
        CheckNextCellEntities(newX, newY);
        SetPosition(_snakeHead, newX, newY);
    }

    private void MoveSnakeTail()
    {
        for (int i = _snakeTail.Count - 1; i > 0; i--)
        {
            int x = _snakeTail[i - 1].currentGridCell.coordinate.x;
            int y = _snakeTail[i - 1].currentGridCell.coordinate.y;

            SetPosition(_snakeTail[i], x, y);
        }
    }

    private void CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = _gridInfo.GetGridCellByCoordinate(x, y);
        if(nextCell == null)return;
        if (nextCell.entityOcupating == null) return;

        if (nextCell.entityOcupating is ICollectable)
        {
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snakeHead);
        }
        else if (nextCell.entityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            _snakeHead.GetComponent<Snake>().Die();
        }
    }

    private void SetPosition(Entity entity, int x, int y)
    {
        entity.currentGridCell = _gridInfo.GetGridCellByCoordinate(x, y);
        entity.transform.position = new Vector3(entity.currentGridCell.coordinate.x, entity.currentGridCell.coordinate.y, 0);
    }
}
