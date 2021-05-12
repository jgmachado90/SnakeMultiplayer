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

    private int _startingX;
    public int StartingX { get { return _startingX; } set { _startingX = value; } }
    
    private int _startingY;
    public int StartingY { get { return _startingY; } set { _startingY = value; } }

    public Color snakeColor;


    [Header("Snake")]
    [SerializeField] private SnakeSettings _snakeSettings;
    public SnakeSettings SnakeSettings { set { _snakeSettings = value; } }

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;


    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    
    public bool dead = false;


    private void Awake()
    {
        _canMove = false;
        _snakeInput = new ControllerInput();
        _snakeMover = new SnakeMover(_snakeInput, _gridManager, this);
        _snakeEater = new SnakeEater(_snakeSettings, this);
    }

    private void Start()
    {
        StartCoroutine(TickCoroutine());
    }

    public void SetStartPosition(int x, int y)
    {
        StartingX = x;
        StartingY = y;
    }

    public void RestartSnake()
    {
        //To do
    }

    public void AssignSnakeInput(KeyCode keyLeft, KeyCode keyRight)
    {
        _snakeInput.LeftKey = keyLeft;
        _snakeInput.RightKey = keyRight;
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

        RestartSnake();
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
