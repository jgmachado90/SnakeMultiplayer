using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    public void CreateEntitiesByGridInstance(GridInstance gridInstance)
    {
        _gridInfo.ClearGrid();

        CreateSnakes(gridInstance);
        CreateCollectables(gridInstance);

    }

    private void CreateCollectables(GridInstance gridInstance)
    {
        foreach(EntitySaveInfo collectable in gridInstance.EveryCollectable)
        {
            int x = collectable.x;
            int y = collectable.y;
            GameObject entityPrefab = collectable.prefab;
            Transform parent = collectable.parent;

            InstantiateCollectablesInTheGrid(entityPrefab, _gridInfo.GetGridCellByCoordinate(x, y), parent);

        }
    }

    private void InstantiateCollectablesInTheGrid(GameObject entityPrefab, GridCell gridCell, Transform parent)
    {
        GameObject newEntityGO = Instantiate(entityPrefab, parent);
        Entity newEntity = newEntityGO.GetComponent<Entity>();
        newEntity.currentGridCell = gridCell;
    }

    private void CreateSnakes(GridInstance gridInstance)
    {
        foreach (List<EntitySaveInfo> snakes in gridInstance.EverySnake)
        {
            SnakePart aux = null;
            foreach (EntitySaveInfo snakePart in snakes)
            {

                int x = snakePart.x;
                int y = snakePart.y;
                GameObject entityPrefab = snakePart.prefab;
                Transform parent = snakePart.parent;
                bool isHead = snakePart.isHead;

                SnakePart instantiatedSnakePart = InstantiateSnakesInTheGrid(entityPrefab, _gridInfo.GetGridCellByCoordinate(x, y), parent, isHead);

                if (snakes.Last() == snakePart)
                {
                    Snake snake = parent.GetComponent<Snake>();
                    snake._startingHead.Prox = instantiatedSnakePart;
                }

                if (aux != null)
                {
                    instantiatedSnakePart.Prox = aux;
                }

                aux = instantiatedSnakePart;
            }
        }
    }

    private SnakePart InstantiateSnakesInTheGrid(GameObject entityPrefab, GridCell gridCell, Transform parent, bool isHead)
    {
        GameObject newEntityGO = Instantiate(entityPrefab, parent);
        Entity newEntity = newEntityGO.GetComponent<Entity>();
        newEntity.currentGridCell = gridCell;

        if (isHead)
        {
            Snake snake = parent.GetComponent<Snake>();
            snake._startingHead = (SnakePart)newEntity;
            snake.StartSnake();
        }

        return (SnakePart)newEntity;

    }
}



