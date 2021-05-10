using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [Header("SnakeInstanceVariables")]
   
    [SerializeField] private SnakeBlock _currentHead;
    public SnakeBlock CurrentHead { get { return _currentHead; } set { _currentHead = value; } }

    [SerializeField] private List<SnakeBlock> _snake;
    public List<SnakeBlock> ThisSnake { get { return _snake; }set { _snake = value; } }

    [SerializeField] private bool _canMove;
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }

    public int startingX;
    public int startingY;

    public Color snakeColor;


    [Header("Snake")]
    [SerializeField] private SnakeSettings _snakeSettings;

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;


    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    
    public bool dead = false;


    private void Awake()
    {
        startingX = -1;
        startingY = -1;

        _canMove = false;
        _snakeInput = new ControllerInput();
        _snakeMover = new SnakeMover(_snakeInput, _gridManager, this);
        _snakeEater = new SnakeEater(_snakeSettings, this);
    }

    private void Start()
    {
        StartCoroutine(TickCoroutine());
    }

    public void StartSnake(KeyCode keyLeft, KeyCode keyRight)
    {
        CurrentHead.IsHead = true;

        ThisSnake.Clear();
        ThisSnake.Add(CurrentHead);

        if (CurrentHead.currentGridCell == null && startingX < 0 && startingY < 0)
        {
            GridCell newStartingCell = _gridManager.GetFarFromEntitiesGridCell();

            newStartingCell = _gridManager.GetGridCellByCoordinate(newStartingCell.coordinate.x, newStartingCell.coordinate.y);

            startingX = newStartingCell.coordinate.x;
            startingY = newStartingCell.coordinate.x;
        }
        else
        {
            CanMove = true;
        }

        PlaceSnakeOnGrid(_gridManager.GetGridCellByCoordinate(startingX, startingY));

        _snakeInput.LeftKey = keyLeft;
        _snakeInput.RightKey = keyRight;
    }

    private void PlaceSnakeOnGrid(GridCell newStartingCell)
    {
        CurrentHead.currentGridCell = newStartingCell;
        GenerateNewSnakePart(newStartingCell.coordinate.x - 1, newStartingCell.coordinate.y, _snakeSettings.FirstBlock);
        GenerateNewSnakePart(newStartingCell.coordinate.x - 2, newStartingCell.coordinate.y, _snakeSettings.SecondBlock);
        GenerateNewSnakePart(newStartingCell.coordinate.x - 3, newStartingCell.coordinate.y, _snakeSettings.ThirdBlock);
    }

    private void GenerateNewSnakePart(int x, int y, CollectableType block)
    {
        GameObject tailPrefab = _snakeSettings.SnakePrefabSettings.TailPrefab;
        GameObject newTailGO = Instantiate(tailPrefab, transform);

        SnakeBlock newTail = newTailGO.GetComponent<SnakeBlock>();

        newTail.BlockType = block;

        newTail.currentGridCell = _gridManager.GetGridCellByCoordinate(x, y);

        ThisSnake.Add(newTail);
    }

    IEnumerator TickCoroutine()
    {
        GridCell newSnakeBlockPosition = null;
        while (true)
        {
            if (CanMove)
            {
                float snakeMovementsPerSec = _snakeEater.GetSnakeCurrentSpeed();
                yield return new WaitForSeconds(snakeMovementsPerSec);

                bool willGrow = _snakeEater.HasFoodInTheLastPosition();
                if (willGrow)
                    newSnakeBlockPosition = ThisSnake.Last().currentGridCell;

                _snakeMover.Tick();


                if (willGrow)
                    Grow(newSnakeBlockPosition);
            }
            yield return null;


        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _canMove = true;
        }
        _snakeInput.ReadInput();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Die();
        }
    }

    internal void Die()
    {
        ReloadThisSnake();
    }

    private void ReloadThisSnake()
    {
        foreach (SnakeBlock parts in ThisSnake)
        {
                Destroy(parts.gameObject);
        }

        _snakeEater._nextTailParts.Clear();

        GameObject newSnakeGO = Instantiate(_snakeSettings.SnakePrefabSettings.TailPrefab, transform);
        SnakeBlock newSnake = newSnakeGO.GetComponent<SnakeBlock>();

        CurrentHead = newSnake;

        StartSnake(_snakeInput.LeftKey, _snakeInput.RightKey);
    }

    public void Feed(Entity collector, CollectableType collectedType)
    {
        _snakeEater._nextTailParts.Add(collectedType);
        SnakeBlock collectorSnakeBlock = (SnakeBlock)collector;
        collectorSnakeBlock.HasFood = true;
    }

    public void Grow(GridCell newBlockPosition)
    {
        GameObject tailPrefab = _snakeSettings.SnakePrefabSettings.TailPrefab;
        GameObject newTailGO = Instantiate(tailPrefab, transform);

        SnakeBlock newTail = newTailGO.GetComponent<SnakeBlock>();

        newTail.BlockType = _snakeEater.NextTailPartsPop();

        SnakeBlock lastSnakePart = ThisSnake.Last();
        lastSnakePart.HasFood = false;

        newTail.currentGridCell = newBlockPosition;

        ThisSnake.Add(newTail);
    }


    internal void CutInThisBlock(SnakeBlock entityOcupating)
    {
        int indexToRemove = 0;
        int i = 0;
        foreach(SnakeBlock snakeBlock in ThisSnake)
        {
            if(snakeBlock == entityOcupating)
            {
                indexToRemove = i;
       
            }
            if(indexToRemove != 0)
                Destroy(snakeBlock.gameObject);

            i++;
        }

        ThisSnake.RemoveRange(indexToRemove, ThisSnake.Count - indexToRemove);
    }
}
