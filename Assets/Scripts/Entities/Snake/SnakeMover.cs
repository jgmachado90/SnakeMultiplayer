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
        if (_snake.CurrentHead != null && _snake.CurrentHead.currentGridCell == null ) return;
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
            BatteringRamEffect(snakeLength, snake, i);
            snake[i].currentGridCell = snake[i - 1].currentGridCell;
        }

    }

    private static void BatteringRamEffect(int snakeLength, List<SnakeBlock> snake, int i)
    {
        if (!snake[i].gameObject.activeSelf && i == snakeLength)
            snake[i].gameObject.SetActive(true);
        else if (!snake[i - 1].gameObject.activeSelf)
        {
            snake[i - 1].gameObject.SetActive(true);
            snake[i].gameObject.SetActive(false);
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

        bool move = CheckNextCellEntities(newX, newY);
        if(!move)
            _snake.CurrentHead.currentGridCell = _gridManager.GetGridCellByCoordinate(newX, newY);
    }

    private bool CheckNextCellEntities(int x, int y)
    {
        GridCell nextCell = _gridManager.GetGridCellByCoordinate(x, y);
        if(nextCell == null)return false;
        if (nextCell.EntityOcupating == null) return false;

        if (nextCell.EntityOcupating is ICollectable)
        {
            nextCell.EntityOcupating.GetComponent<ICollectable>().Collect(_snake.CurrentHead);
        }

        else if (nextCell.EntityOcupating.typeOfEntity == Entity.TypeOfEntity.Player)
        {
            if (_snake.GetComponent<BatteringRamPowerUpController>().BlockQuantity > 0)
            {
                SnakeBlock enemySnakeBlock = nextCell.EntityOcupating.GetComponent<SnakeBlock>();
                enemySnakeBlock.gameObject.SetActive(false);
           
                _snake.CurrentHead.GetComponentInParent<BatteringRamPowerUpController>().RemovingBatteringRamPowerUp();
            }
            else if(_snake.GetComponent<TimeTravelPowerUpController>().BlockQuantity > 0)
            {
                TimeTravelPowerUpController timeTravel = _snake.GetComponent<TimeTravelPowerUpController>();
                timeTravel.StartTimeTravel();
                return true;
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
