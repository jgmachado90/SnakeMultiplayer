using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Entity
{
    [SerializeField] private SnakeSettings _snakeSettings;
   

    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;

    private Coroutine _tickCoroutine;

    private List<Entity> _tail = new List<Entity>();
    private List<GridCellCoordinates> _growPositions = new List<GridCellCoordinates>();
    

    private void Awake()
    {
        _snakeInput = _snakeSettings.IsAI ? new AiInput() as ISnakeInput : new ControllerInput();
        
        _tail.Add(this);
        _snakeMover = new SnakeMover(_snakeInput, _tail, _snakeSettings);

        _tickCoroutine = StartCoroutine(TickCoroutine());
    }

    private void Start()
    {
    }

    IEnumerator TickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_snakeSettings.MovementsPerSecond);

            if(currentGridCell == null)
                currentGridCell = GridGenerator.instance.GetGridCellByCoordinate(_snakeSettings.StartX, _snakeSettings.StartY);

            GridCell gridCellToGrow = GetFoodGridCellInTheLastTailPosition();

            _snakeMover.Tick();

            if (gridCellToGrow != null)
                Grow(gridCellToGrow);

        }
    }

    private GridCell GetFoodGridCellInTheLastTailPosition()
    {
        Entity lastSnakeTail = _tail[_tail.Count - 1];

        foreach (GridCellCoordinates growPositions in _growPositions)
        {
           if(  lastSnakeTail.currentGridCell.coordinate.x == growPositions.x &&
                lastSnakeTail.currentGridCell.coordinate.y == growPositions.y   )
            {
                return lastSnakeTail.currentGridCell;
            }
        }
        return null;
    }

    private void Grow(GridCell newTailGridCell)
    {
        Debug.Log("Grow");
        GameObject newTailGO = Instantiate(_snakeSettings.TailPrefab, null);
        SnakeTail newTail = newTailGO.GetComponent<SnakeTail>();
        newTail.currentGridCell = newTailGridCell;
        newTail.Prox = _tail[_tail.Count - 1];
        _tail.Add(newTail);
        _growPositions.Remove(newTailGridCell.coordinate);


    }

    private void Update()
    {
        _snakeInput.ReadInput();
    }

    internal void Die()
    {
        throw new NotImplementedException();
    }

    internal void Feed(GridCell feedCell)
    {
        _growPositions.Add(feedCell.coordinate);
    }

    
}
