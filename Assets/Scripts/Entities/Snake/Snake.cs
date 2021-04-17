using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    private List<Entity> snakeParts = new List<Entity>();



    private void Awake()
    {
        _snakeInput = _snakeSettings.IsAI ? new Player2Input() as ISnakeInput : new ControllerInput();

        StartSnake();

        _snakeMover = new SnakeMover(_snakeInput, snakeParts, _snakeSettings, _gridInfo);
        _snakeEater = new SnakeEater(snakeParts, _snakeSettings);

        _tickCoroutine = StartCoroutine(TickCoroutine());
    }

    public void StartSnake()
    {
        _startingHead.Prox = _startingHead;
        _snakeSettings.CurrentHead = _startingHead;
        snakeParts.Add(_startingHead);
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
    }

    private void ReloadThisSnake()
    {
        foreach(SnakePart parts in snakeParts)
        {
            if (parts.isHead)
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

        SnakePart lastSnakePart = (SnakePart)_snakeSettings.CurrentHead.Prox;
        lastSnakePart.transform.localScale = new Vector3(_snakeSettings.StartScale.Value, _snakeSettings.StartScale.Value, _snakeSettings.StartScale.Value);
        lastSnakePart.hasFood = false;

        newTail.currentGridCell = newTailGridCell; 
        newTail.Prox = lastSnakePart;
       
        snakeParts.Add(newTail);
        _snakeSettings.CurrentHead.Prox = newTail;

    }


    


}
