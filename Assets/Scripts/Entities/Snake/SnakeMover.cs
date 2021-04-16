using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover
{
    private readonly ISnakeInput _snakeInput;
    private readonly List<Entity> _snakePart;
    private readonly GridInfo _gridInfo;
    private readonly SnakeSettings _snakeSettings;

    private Direction lookingDirection;

    public SnakeMover(ISnakeInput snakeInput, List<Entity> snakePart, SnakeSettings snakeSettings, GridInfo gridInfo)
    {
        _snakeInput = snakeInput;
        _snakePart = snakePart;
        _gridInfo = gridInfo;
        _snakeSettings = snakeSettings;
    }

    public void Tick()
    {
        if (_snakeSettings.CurrentHead.currentGridCell == null) return;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        int newX = _snakeSettings.CurrentHead.currentGridCell.coordinate.x;
        int newY = _snakeSettings.CurrentHead.currentGridCell.coordinate.y;

        if (_snakeInput.Direction == Direction.Up && lookingDirection != Direction.Down)
        {
            newY++;
            lookingDirection = Direction.Up;
            _snakeSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.Direction == Direction.Down && lookingDirection != Direction.Up)
        {
            newY--;
            lookingDirection = Direction.Down;
            _snakeSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.Direction == Direction.Left && lookingDirection != Direction.Right)
        {
            newX--;
            lookingDirection = Direction.Left;
            _snakeSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.Direction == Direction.Right && lookingDirection != Direction.Left)
        {
            lookingDirection = Direction.Right;
            newX++;
            _snakeSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        MoveSnakeHead(newX, newY);
    }

    private void MoveSnakeHead(int newX, int newY)
    {
        CheckNextCellEntities(newX, newY);
        SetPosition(_snakeSettings.CurrentHead, newX, newY);
    }

    private void CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = _gridInfo.GetGridCellByCoordinate(x, y);
        if(nextCell == null)return;
        if (nextCell.entityOcupating == null) return;

        if (nextCell.entityOcupating is ICollectable)
        {
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snakeSettings.CurrentHead.Prox);
        }
        else if (nextCell.entityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            _snakeSettings.CurrentHead.GetComponentInParent<Snake>().Die();
        }
    }

    private void SetPosition(Entity entity, int x, int y)
    {
        SnakePart lastPart = (SnakePart)_snakeSettings.CurrentHead.Prox;

        lastPart.currentGridCell = _gridInfo.GetGridCellByCoordinate(x, y);
        lastPart.transform.position = new Vector3(lastPart.currentGridCell.coordinate.x, lastPart.currentGridCell.coordinate.y, 0);

        _snakeSettings.CurrentHead.isHead = false;
        _snakeSettings.CurrentHead = lastPart;
        lastPart.isHead = true;
    }
}
