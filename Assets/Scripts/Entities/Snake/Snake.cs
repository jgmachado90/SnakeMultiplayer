using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [Header("SnakeInstanceVariables")]
    [SerializeField] private int _snakeIndex;
    public int Snakeindex { get { return _snakeIndex; } set { _snakeIndex = value; } }
   
    [SerializeField] private SnakeBlock _currentHead;
    public SnakeBlock CurrentHead { get { return _currentHead; } set { _currentHead = value; } }

    [SerializeField] private List<SnakeBlock> _snake;
    public List<SnakeBlock> ThisSnake { get { return _snake; }set { _snake = value; } }


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
        _snakeInput = _snakeSettings.IsPlayer2 ? new Player2Input() as ISnakeInput : new ControllerInput();

        _snakeMover = new SnakeMover(_snakeInput, _gridManager, this);
        _snakeEater = new SnakeEater(_snakeSettings, this);

        StartCoroutine(TickCoroutine());
    }

    public void StartSnake()
    {
        CurrentHead.IsHead = true;
        ThisSnake.Clear();
        ThisSnake.Add(CurrentHead);
       
        if (CurrentHead.currentGridCell == null)
            CurrentHead.currentGridCell = _gridManager.GetGridCellByCoordinate(_snakeSettings.SnakeMovementSettings.StartX.Value, _snakeSettings.SnakeMovementSettings.StartY.Value);

    }

    IEnumerator TickCoroutine()
    {
        GridCell newSnakeBlockPosition = null;
        while (true)
        {
            float snakeMovementsPerSec = _snakeEater.GetSnakeCurrentSpeed();
            yield return new WaitForSeconds(snakeMovementsPerSec);

            bool willGrow = _snakeEater.HasFoodInTheLastPosition();
            if(willGrow)
             newSnakeBlockPosition = ThisSnake.Last().currentGridCell;

             _snakeMover.Tick();


            if (willGrow)
                Grow(newSnakeBlockPosition);

           
        }
    }

    private void Update()
    {
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

        StartSnake();
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
