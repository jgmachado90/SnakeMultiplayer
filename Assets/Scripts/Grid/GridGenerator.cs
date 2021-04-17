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
    [SerializeField] private GridInstance _gridInstance;

    public GameObject foodPrefab;
    public GameObject enginePowerPrefab;
    public GameObject snakePartPrefab;

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
        _gridInfo.ClearGrid();

        CreateSnakes();
        CreateCollectables();

    }

    private void CreateCollectables()
    {
        foreach(Tuple<int, int, GameObject, Transform> collectable in _gridInstance.EveryCollectable)
        {
            int x = collectable.Item1;
            int y = collectable.Item2;
            GameObject entityPrefab = collectable.Item3;
            Transform parent = collectable.Item4;

            InstantiateCollectablesInTheGrid(entityPrefab, _gridInfo.GetGridCellByCoordinate(x, y), parent);

        }
    }

    private void InstantiateCollectablesInTheGrid(GameObject entityPrefab, GridCell gridCell, Transform parent)
    {
        GameObject newEntityGO = Instantiate(entityPrefab, parent);
        Entity newEntity = newEntityGO.GetComponent<Entity>();
        newEntity.currentGridCell = gridCell;
    }

    private void CreateSnakes()
    {
        foreach (List<Tuple<int, int, GameObject, Transform, bool>> snakes in _gridInstance.EverySnake)
        {
            SnakePart aux = null;
            foreach (Tuple<int, int, GameObject, Transform, bool> snakePart in snakes)
            {

                int x = snakePart.Item1;
                int y = snakePart.Item2;
                GameObject entityPrefab = snakePart.Item3;
                Transform parent = snakePart.Item4;
                bool isHead = snakePart.Item5;

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



