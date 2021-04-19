using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeMover
{
    private readonly Snake _snake;
    private readonly ISnakeInput _snakeInput;
    private readonly GridManager _gridManager;

    public SnakeMover(ISnakeInput snakeInput, GridManager gridManager, Snake snake)
    {
        _snakeInput = snakeInput;
        _gridManager = gridManager;
        _snake = snake;
    }

    public void Tick()
    {
        if (_snake.CurrentHead.currentGridCell == null) return;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        int newX = _snake.CurrentHead.currentGridCell.coordinate.x;
        int newY = _snake.CurrentHead.currentGridCell.coordinate.y;

        PrepareMovement(ref newX, ref newY);

        MoveFoodBlock();

        if (_snake.ThisSnake.Count - 1 > 0)
            MoveSnakeBlocks();

        MoveSnakeHead(newX, newY);
        
    }

    private void PrepareMovement(ref int newX, ref int newY)
    {
        if (_snakeInput.MovingDirection == Direction.Up)
        {
            newY++;
            _snakeInput.LookingDirection = Direction.Up;
            _snake.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_snakeInput.MovingDirection == Direction.Down)
        {
            newY--;
            _snakeInput.LookingDirection = Direction.Down;
            _snake.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (_snakeInput.MovingDirection == Direction.Left)
        {
            newX--;
            _snakeInput.LookingDirection = Direction.Left;
            _snake.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (_snakeInput.MovingDirection == Direction.Right)
        {
            newX++;
            _snakeInput.LookingDirection = Direction.Right;
            _snake.CurrentHead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void MoveSnakeBlocks()
    {
        int snakeLength = _snake.ThisSnake.Count - 1;
        List<SnakeBlock> snake = _snake.ThisSnake;

        for (int i = snakeLength; i > 0; i--)
        {
            snake[i].currentGridCell = snake[i - 1].currentGridCell;
        }

    }

    private void MoveFoodBlock()
    {
        int snakeLength = _snake.ThisSnake.Count - 1;
        List<SnakeBlock> snake = _snake.ThisSnake;

        for (int i = snakeLength; i >= 0; i--)
        {
            if (snake[i] != _snake.ThisSnake.Last() && snake[i].HasFood)
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
            _snake.CurrentHead.currentGridCell = _gridManager.GetGridCellByCoordinate(newX, newY);
    }

    private bool CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = _gridManager.GetGridCellByCoordinate(x, y);
        if(nextCell == null)return false;
        if (nextCell.entityOcupating == null) return false;

        if (nextCell.entityOcupating is ICollectable)
        {
            nextCell.entityOcupating.GetComponent<ICollectable>().Collect(_snake.CurrentHead);
        }

        else if (nextCell.entityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            Debug.Log("NextCell Is Player");
            if (_snake.GetComponent<BatteringRamPowerUpController>().BatteringRam > 0)
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
                _snake.CurrentHead.GetComponentInParent<BatteringRamPowerUpController>().RemovingBatteringRamPowerUp();
            }
            else
            {
                _snake.CurrentHead.GetComponentInParent<Snake>().Die();
                return true;
            }
        }
        return false;
    }
}
