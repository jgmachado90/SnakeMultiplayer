using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover
{
    private readonly ISnakeInput _snakeInput;
    private readonly GridInfo _gridInfo;
    private readonly SnakeSettings _snakeSettings;


    public SnakeMover(ISnakeInput snakeInput, SnakeSettings snakeSettings, GridInfo gridInfo)
    {
        _snakeInput = snakeInput;
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

        PrepareMovement(ref newX, ref newY);
        MoveSnakeHead(newX, newY);
    }

    private void PrepareMovement(ref int newX, ref int newY)
    {
        if (_snakeInput.MovingDirection == Direction.Up)
        {
            newY++;
            _snakeInput.LookingDirection = Direction.Up;
            _snakeSettings.CurrentHead.Prev.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.MovingDirection == Direction.Down)
        {
            newY--;
            _snakeInput.LookingDirection = Direction.Down;
            _snakeSettings.CurrentHead.Prev.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.MovingDirection == Direction.Left)
        {
            newX--;
            _snakeInput.LookingDirection = Direction.Left;
            _snakeSettings.CurrentHead.Prev.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.MovingDirection == Direction.Right)
        {
            newX++;
            _snakeInput.LookingDirection = Direction.Right;
            _snakeSettings.CurrentHead.Prev.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
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
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snakeSettings.CurrentHead.Prev);
        }

        else if (nextCell.entityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            _snakeSettings.CurrentHead.GetComponentInParent<Snake>().Die();
        }
    }

    private void SetPosition(Entity entity, int x, int y)
    {
        SnakePart lastPart = _snakeSettings.CurrentHead.Prev;

        lastPart.currentGridCell = _gridInfo.GetGridCellByCoordinate(x, y);
        lastPart.transform.position = new Vector3(lastPart.currentGridCell.coordinate.x, lastPart.currentGridCell.coordinate.y, 0);

        _snakeSettings.CurrentHead.IsHead = false;
        _snakeSettings.CurrentHead = lastPart;
        lastPart.IsHead = true;
    }
}
