using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridSettings _gridSettings;
    [SerializeField] private GridInfo _gridInfo;
    [SerializeField] private GridInstance _gridInstance;

    public GameObject foodPrefab;
    public GameObject enginePowerPrefab;
    public GameObject PlayerPrefab;
    public GameObject PlayerTail;

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
    public void CreateEntitiesByGridInstance()
    {
         Debug.Log("On time travel");
        _gridInfo.ClearGrid();
        for(int i=0; i < _gridInstance.EntitiesInGrid.Count; i++)
        {
            InstantiateEntityInGrid(_gridInstance.EntitiesInGrid[i], _gridInstance.EntitiesGridCell[i]);
        }
      
    }

    private void InstantiateEntityInGrid(Entity entity, GridCell gridCell)
    {
        GameObject newEntityGO = Instantiate(entity.EntityInfo.EntityPrefab);
        Entity newEntity = newEntityGO.GetComponent<Entity>();
        newEntity.currentGridCell = gridCell;
    }
}



