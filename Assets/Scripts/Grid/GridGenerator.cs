using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridSettings _gridSettings;
    [SerializeField] private GridInfo _gridInfo;

    [Header("Events")]
    public VoidEvent OnCreateGrid;

    private void Start()
    {
        _gridInfo._gridCells.Clear();
        _gridInfo._emptyCells.Clear();
        GenerateScenery();
        OnCreateGrid.Raise();
    }

    private void GenerateScenery()
    {
        for(int j = 0; j < _gridSettings.LengthX.Value; j++)
        {
            for(int i = 0; i < _gridSettings.LengthY.Value; i++)
            {
                GameObject gridPrefab = Instantiate(_gridSettings.GridPrefab, transform);
                gridPrefab.transform.position = new Vector3(i,j,0);
                GridCell gridCell = gridPrefab.GetComponent<GridCell>();
                gridCell.coordinate.x = i;
                gridCell.coordinate.y = j;
                _gridInfo._gridCells.Add(gridCell);
                _gridInfo._emptyCells.Add(gridCell);
            }
        }
    }

    

}



