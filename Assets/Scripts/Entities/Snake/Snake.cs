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
    [SerializeField] private GridInfo _gridInfo;


    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    private Coroutine _tickCoroutine;

    public SnakePart _startingHead;


    private void Awake()
    {
        _snakeSettings.Clear();
        _snakeInput = _snakeSettings.IsAI ? new Player2Input() as ISnakeInput : new ControllerInput();

        StartSnake();

        _snakeMover = new SnakeMover(_snakeInput, _snakeSettings, _gridInfo);
        _snakeEater = new SnakeEater(_snakeSettings);

        _tickCoroutine = StartCoroutine(TickCoroutine());
    }

    public void StartSnake()
    {
        _startingHead.IsHead = true;
        _startingHead.Prox = _startingHead;
        _startingHead.Prev = _startingHead;
        _snakeSettings.CurrentHead = _startingHead;
        _snakeSettings.Snake.Add(_startingHead);
    }

    IEnumerator TickCoroutine()
    {
        while (true)
        {
            float snakeMovementsPerSec = _snakeEater.GetSnakeCurrentSpeed();
            yield return new WaitForSeconds(snakeMovementsPerSec);

            if(_snakeSettings.CurrentHead.currentGridCell == null)
                _snakeSettings.CurrentHead.currentGridCell = _gridInfo.GetGridCellByCoordinate(_snakeSettings.StartX.Value, _snakeSettings.StartY.Value);

            _snakeMover.Tick();

            GridCell gridCellToGrow = _snakeEater.GetFoodGridCellInTheLastTailPosition();

            if (gridCellToGrow != null)
                Grow(gridCellToGrow, _snakeSettings.TailPrefab);

           
        }
    }

    private void Update()
    {
        _snakeInput.ReadInput();
    }

    internal void Die()
    {
        //ReloadThisSnake();
        GetComponent<TimeTravelPowerUpController>().ActivatePowerUp();
    }

    private void ReloadThisSnake()
    {
        foreach(SnakePart parts in _snakeSettings.Snake)
        {
            if (parts.IsHead)
            {
                _snakeSettings.CurrentHead.currentGridCell = _gridInfo.GetGridCellByCoordinate(_snakeSettings.StartX.Value, _snakeSettings.StartY.Value);
            }
            Destroy(parts.gameObject);
        }

    }

    public void Feed(GridCell currentGridCell, Entity collector)
    {
        _snakeEater.AddGrowCoordinate(currentGridCell.coordinate, collector);
    }

    public void Grow(GridCell newTailGridCell, GameObject tailPrefab)
    {
        GameObject newTailGO = Instantiate(tailPrefab, transform);
        SnakePart newTail = newTailGO.GetComponent<SnakePart>();

        SnakePart lastSnakePart = _snakeSettings.CurrentHead.Prev;
        lastSnakePart.transform.localScale = new Vector3(_snakeSettings.StartScale.Value, _snakeSettings.StartScale.Value, _snakeSettings.StartScale.Value);
        lastSnakePart.hasFood = false;

        newTail.currentGridCell = newTailGridCell;

        if (_snakeSettings.CurrentHead.Prox == _snakeSettings.CurrentHead)
        {
            _snakeSettings.CurrentHead.Prox = newTail;
            _snakeSettings.CurrentHead.Prev = newTail;

            newTail.Prox = _snakeSettings.CurrentHead;
            newTail.Prev = _snakeSettings.CurrentHead;
        }

        else
        {
            SnakePart oldTail = _snakeSettings.CurrentHead.Prev;

            oldTail.Prox = newTail;
            _snakeSettings.CurrentHead.Prev = newTail;

            newTail.Prev = oldTail;
            newTail.Prox = _snakeSettings.CurrentHead;
        
        }

        _snakeSettings.Snake.Add(newTail);

    

    }


    


}
