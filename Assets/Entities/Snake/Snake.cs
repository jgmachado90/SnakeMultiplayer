﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Entity
{
    [SerializeField] private SnakeSettings _snakeSettings;
   

    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    private Coroutine _tickCoroutine;

    private List<Entity> _tail = new List<Entity>();

    

    private void Awake()
    {
        _snakeInput = _snakeSettings.IsAI ? new AiInput() as ISnakeInput : new ControllerInput();
        
        _tail.Add(this);
        _snakeMover = new SnakeMover(_snakeInput, _tail, _snakeSettings);
        _snakeEater = new SnakeEater(_tail, _snakeSettings);

        _tickCoroutine = StartCoroutine(TickCoroutine());
    }

    IEnumerator TickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_snakeSettings.MovementsPerSecond);

            if(currentGridCell == null)
                currentGridCell = GridGenerator.instance.GetGridCellByCoordinate(_snakeSettings.StartX, _snakeSettings.StartY);

            GridCell gridCellToGrow = _snakeEater.GetFoodGridCellInTheLastTailPosition();

            _snakeMover.Tick();

            _snakeEater.ScaleTailsWithFood();

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
        throw new NotImplementedException();
    }

    public void Feed(GridCell currentGridCell)
    {
        _snakeEater.AddGrowCoordinate(currentGridCell.coordinate);
    }

    public void Grow(GridCell newTailGridCell, GameObject tailPrefab)
    {
        GameObject newTailGO = Instantiate(tailPrefab, null);
        SnakeTail newTail = newTailGO.GetComponent<SnakeTail>();

        newTail.currentGridCell = newTailGridCell;
        newTail.Prox = _tail[_tail.Count - 1];

        _tail.Add(newTail);
        _snakeEater.RemoveGrowCoordinate(newTailGridCell.coordinate);
        
    }



}
