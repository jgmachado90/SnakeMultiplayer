using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [Header("Snake")]
    [SerializeField] private SnakeSettings _snakeSettings;
    

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;


    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    public SnakeBlock _startingHead;

    public bool dead = false;


    private void Awake()
    {
        _snakeSettings.SnakeInGameSettings.Clear();

        _snakeInput = _snakeSettings.IsPlayer2 ? new Player2Input() as ISnakeInput : new ControllerInput();

        //StartSnake();

        _snakeMover = new SnakeMover(_snakeInput, _snakeSettings, _gridManager);
        _snakeEater = new SnakeEater(_snakeSettings);

        StartCoroutine(TickCoroutine());
    }

    public void StartSnake()
    {
        _startingHead.IsHead = true;
        _snakeSettings.SnakeInGameSettings.CurrentHead = _startingHead;
        _snakeSettings.SnakeInGameSettings.Snake.Add(_startingHead);
       
        if (_snakeSettings.SnakeInGameSettings.CurrentHead.currentGridCell == null)
            _snakeSettings.SnakeInGameSettings.CurrentHead.currentGridCell = _gridManager.GetGridCellByCoordinate(_snakeSettings.SnakeMovementSettings.StartX.Value, _snakeSettings.SnakeMovementSettings.StartY.Value);

    }

    IEnumerator TickCoroutine()
    {
        GridCell newSnakeBlockPosition = null;// = new GridCell();
        while (true)
        {
            float snakeMovementsPerSec = _snakeEater.GetSnakeCurrentSpeed();
            yield return new WaitForSeconds(snakeMovementsPerSec);

            bool willGrow = _snakeEater.HasFoodInTheLastPosition();
            if(willGrow)
             newSnakeBlockPosition = _snakeSettings.SnakeInGameSettings.Snake.Last().currentGridCell;

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
        foreach (SnakeBlock parts in _snakeSettings.SnakeInGameSettings.Snake)
        {
                Destroy(parts.gameObject);
        }

        _snakeSettings.SnakeInGameSettings.Snake.Clear();
        _snakeEater._nextTailParts.Clear();
        _snakeSettings.SnakeInGameSettings.Clear(); 


        GameObject newSnakeGO = Instantiate(_snakeSettings.SnakePrefabSettings.TailPrefab, transform);
        SnakeBlock newSnake = newSnakeGO.GetComponent<SnakeBlock>();

        _startingHead = newSnake;

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

        SnakeBlock lastSnakePart = _snakeSettings.SnakeInGameSettings.Snake.Last();
        lastSnakePart.HasFood = false;

        newTail.currentGridCell = newBlockPosition;

        _snakeSettings.SnakeInGameSettings.Snake.Add(newTail);
    }


    internal void CutInThisBlock(SnakeBlock entityOcupating)
    {
        int indexToRemove = 0;
        int i = 0;
        foreach(SnakeBlock snakeBlock in _snakeSettings.SnakeInGameSettings.Snake)
        {
            if(snakeBlock == entityOcupating)
            {
                indexToRemove = i;
       
            }
            if(indexToRemove != 0)
                Destroy(snakeBlock.gameObject);

            i++;
        }

        _snakeSettings.SnakeInGameSettings.Snake.RemoveRange(indexToRemove, _snakeSettings.SnakeInGameSettings.Snake.Count - indexToRemove);
    }
}
