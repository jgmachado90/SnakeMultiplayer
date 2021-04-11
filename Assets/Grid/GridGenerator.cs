using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator instance;

    [SerializeField] private GridSettings _gridSettings;

    private List<GridCell> _gridCells = new List<GridCell>();

    public GameObject food;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateScenery();

        instantiateNewFood();
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
            }
        }
    }

    public void instantiateNewFood()
    {
        GameObject foodGO = Instantiate(food);
        Food newfood = foodGO.GetComponent<Food>();
        newfood.currentGridCell = _gridCells[25];

        GameObject foodGO2 = Instantiate(food);
        Food newfood2 = foodGO2.GetComponent<Food>();
        newfood2.currentGridCell = _gridCells[36];

        GameObject foodGO3 = Instantiate(food);
        Food newfood3 = foodGO3.GetComponent<Food>();
        newfood3.currentGridCell = _gridCells[18];

    }

    public GridCell GetGridCellByCoordinate(int x, int y)
    {
        return _gridCells[(_gridSettings.LengthX * y) + x];
    }



}



