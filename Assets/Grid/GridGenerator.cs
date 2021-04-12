using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator instance;

    [SerializeField] private GridSettings _gridSettings;

    private List<GridCell> _gridCells = new List<GridCell>();

    private List<GridCell> _emptyCells = new List<GridCell>();

    public Action OnCreateGrid;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateScenery();
        foreach(GridCell gridCells in _gridCells)
        {
            gridCells.OnAddEntity += RemoveCellFromEmptyList;
            gridCells.OnRemoveEntity += AddCellInEmptyList;
        }
        OnCreateGrid?.Invoke();
    }

    private void AddCellInEmptyList(GridCell gridCell)
    {
        _emptyCells.Add(gridCell);
    }

    private void RemoveCellFromEmptyList(GridCell gridCell)
    {
        _emptyCells.Remove(gridCell);
    }

    private void GenerateScenery()
    {
        for(int j = 0; j < _gridSettings.LengthX; j++)
        {
            for(int i = 0; i < _gridSettings.LengthY; i++)
            {
                GameObject gridPrefab = Instantiate(_gridSettings.GridPrefab, transform);
                gridPrefab.transform.position = new Vector3(i,j,0);
                GridCell gridCell = gridPrefab.GetComponent<GridCell>();
                gridCell.coordinate.x = i;
                gridCell.coordinate.y = j;
                _gridCells.Add(gridCell);
                _emptyCells.Add(gridCell);
            }
        }
    }

  

    public GridCell GetGridCellByCoordinate(int x, int y)
    {
        return _gridCells[(_gridSettings.LengthX * y) + x];
    }

    public GridCell GetRandomEmptyGridCell()
    {
        int emptyCellsCount = _emptyCells.Count;

        int indexRNG = UnityEngine.Random.Range(0, emptyCellsCount);


        return _emptyCells[indexRNG];
    }



}



