using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInitializer : MonoBehaviour
{
    public Snake snake;

    [SerializeField] private SnakeSettings _snakeSettings;
    private SnakeStartingBlocks _snakeStartingBlocks;
    private Color _snakeColor;

    public SnakeStartingBlocks SnakeStartingBlocks { get { return _snakeStartingBlocks; } }

    public GridManager _gridManager;

    private void Awake()
    {
        snake.SnakeColor = new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            1
        );
    }


    public void InitializeSnake(SnakeStartingBlocks snakeStartingBlocks)
    {
        _snakeStartingBlocks = snakeStartingBlocks;
        GridCell startingCell = _gridManager.GetFarFromEntitiesGridCell();
        snake.SetStartPosition(startingCell.coordinate.x, startingCell.coordinate.y);
        PlaceSnakeOnGrid(startingCell);
        PaintSnake();
 
    }

    private void PaintSnake()
    {
        foreach (SnakeBlock snakeBlock in snake.ThisSnake)
        {
            snakeBlock.GetComponent<SpriteRenderer>().color = snake.SnakeColor;
        }
    }

    public void AssignInputKeys(KeyCode keyLeft, KeyCode keyRight)
    {
        snake.AssignSnakeInput(keyLeft, keyRight);
    }
    private void PlaceSnakeOnGrid(GridCell newStartingCell)
    {
        snake.CurrentHead.currentGridCell = newStartingCell;

        int x = newStartingCell.coordinate.x;
        int y = newStartingCell.coordinate.y;

        InstantiateNewSnake(x - 1, y, _snakeStartingBlocks.FirstBlock);
        InstantiateNewSnake(x - 2, y, _snakeStartingBlocks.SecondBlock);
        InstantiateNewSnake(x - 3, y, _snakeStartingBlocks.ThirdBlock);
    }

    private void InstantiateNewSnake(int x, int y, CollectableType block)
    {
        GameObject tailPrefab = _snakeSettings.SnakePrefabSettings.TailPrefab;
        GameObject newTailGO = Instantiate(tailPrefab, transform);
        SnakeBlock newTail = newTailGO.GetComponent<SnakeBlock>();
        newTail.BlockType = block;
        ActivateBlockPower(block);
        newTail.currentGridCell = _gridManager.GetGridCellByCoordinate(x, y);
        snake.ThisSnake.Add(newTail);
    }

    private void ActivateBlockPower(CollectableType block)
    {
        if(block == CollectableType.BatteringRam)
            GetComponent<BatteringRamPowerUpController>().CollectPowerUp();

        else if(block == CollectableType.EnginePower)
            GetComponent<EnginePowerUpController>().CollectPowerUp();
    }

    private void DeactivateBlockPower(CollectableType block)
    {
        if (block == CollectableType.BatteringRam)
            GetComponent<BatteringRamPowerUpController>().ClearPowerUp();

        else if (block == CollectableType.EnginePower)
            GetComponent<EnginePowerUpController>().ClearPowerUp();
    }

    public void ChangeSnake(SnakeStartingBlocks newSnakeStartingBlocks)
    {
        _snakeStartingBlocks = newSnakeStartingBlocks;
        DestroyLastSnake();
        GridCell startingCell = _gridManager.GetGridCellByCoordinate(snake.StartingX, snake.StartingY);
        PlaceSnakeOnGrid(startingCell);
        PaintSnake();
    }

    private void DestroyLastSnake()
    {
        SnakeBlock head = null;
        foreach (SnakeBlock snakeBlock in snake.ThisSnake)
        {
            if (!snakeBlock.IsHead)
            {
                Destroy(snakeBlock.gameObject);
                DeactivateBlockPower(snakeBlock.BlockType);
            }
            else
                head = snakeBlock;
        }
        snake.ThisSnake.Clear();
        snake.ThisSnake.Add(head);

    }
}
