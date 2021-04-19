using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeMover
{
    private readonly ISnakeInput _snakeInput;
    private readonly GridManager _gridManager;
    private readonly SnakeInGameSettings _snakeInGameSettings;


    public SnakeMover(ISnakeInput snakeInput, SnakeSettings snakeSettings, GridManager gridManager)
    {
        _snakeInput = snakeInput;
        _gridManager = gridManager;
        _snakeInGameSettings = snakeSettings.SnakeInGameSettings;
    }

    public void Tick()
    {
        if (_snakeInGameSettings.CurrentHead.currentGridCell == null) return;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        int newX = _snakeInGameSettings.CurrentHead.currentGridCell.coordinate.x;
        int newY = _snakeInGameSettings.CurrentHead.currentGridCell.coordinate.y;

        PrepareMovement(ref newX, ref newY);

        MoveFoodBlock();

        if (_snakeInGameSettings.Snake.Count - 1 > 0)
            MoveSnakeBlocks();

        MoveSnakeHead(newX, newY);
        
    }

    private void PrepareMovement(ref int newX, ref int newY)
    {
        if (_snakeInput.MovingDirection == Direction.Up)
        {
            newY++;
            _snakeInput.LookingDirection = Direction.Up;
            _snakeInGameSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.MovingDirection == Direction.Down)
        {
            newY--;
            _snakeInput.LookingDirection = Direction.Down;
            _snakeInGameSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.MovingDirection == Direction.Left)
        {
            newX--;
            _snakeInput.LookingDirection = Direction.Left;
            _snakeInGameSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.MovingDirection == Direction.Right)
        {
            newX++;
            _snakeInput.LookingDirection = Direction.Right;
            _snakeInGameSettings.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void MoveSnakeBlocks()
    {
        int snakeLength = _snakeInGameSettings.Snake.Count - 1;
        List<SnakeBlock> snake = _snakeInGameSettings.Snake;

        for (int i = snakeLength; i > 0; i--)
        {
            snake[i].currentGridCell = snake[i - 1].currentGridCell;
        }

    }

    private void MoveFoodBlock()
    {
        int snakeLength = _snakeInGameSettings.Snake.Count - 1;
        List<SnakeBlock> snake = _snakeInGameSettings.Snake;

        for (int i = snakeLength; i >= 0; i--)
        {
            if (snake[i] != _snakeInGameSettings.Snake.Last() && snake[i].HasFood)
            {
                snake[i].HasFood = false;
                snake[i + 1].HasFood = true;
            }
        }
    }

    private void MoveSnakeHead(int newX, int newY)
    {

        bool dead = CheckNextCellEntities(newX, newY);
        if(!dead)
            _snakeInGameSettings.CurrentHead.currentGridCell = _gridManager.GetGridCellByCoordinate(newX, newY);
    }

    private bool CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = _gridManager.GetGridCellByCoordinate(x, y);
        if(nextCell == null)return false;
        if (nextCell.entityOcupating == null) return false;

        if (nextCell.entityOcupating is ICollectable)
        {
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snakeInGameSettings.CurrentHead);
        }

        else if (nextCell.entityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            Debug.Log("NextCell Is Player");
            if (_snakeInGameSettings.BatteringRam > 0)
            {
                Snake enemySnake = nextCell.entityOcupating.GetComponentInParent<Snake>();
                SnakeBlock enemySnakeBlock = nextCell.entityOcupating.GetComponent<SnakeBlock>();
                if (enemySnakeBlock.IsHead)
                {
                    enemySnake.Die();
                }
                else
                {
                    enemySnakeBlock.gameObject.SetActive(false);
                    enemySnake.CutInThisBlock((SnakeBlock)nextCell.entityOcupating);
                }
                _snakeInGameSettings.CurrentHead.GetComponentInParent<BatteringRamPowerUpController>().RemovingBatteringRamPowerUp();
            }
            else
            {
                _snakeInGameSettings.CurrentHead.GetComponentInParent<Snake>().Die();
                return true;
            }
        }
        return false;
    }
}
