using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Manager", fileName = "GridManager")]
public class GridManager : ScriptableObject
{
    [SerializeField] public GridSettings _gridSettings;

    public List<GridCell> _gridCells = new List<GridCell>();
    public List<GridCell> _emptyCells = new List<GridCell>();

    public int lastY;
    public int sideX;

    public void AddCellInEmptyList(GridCell gridCell)
    {
        _emptyCells.Add(gridCell);
    }

    public void RemoveCellFromEmptyList(GridCell gridCell)
    {
        _emptyCells.Remove(gridCell);
    }

    public GridCell GetGridCellByCoordinate(int x, int y)
    {
        CheckOutOfGridPossibilities(ref x, ref y);

        return _gridCells[(_gridSettings.LengthX.Value * y) + x];
    }

    private void CheckOutOfGridPossibilities(ref int x, ref int y)
    {
        if (x % _gridSettings.LengthX.Value == 0)
            x = x % _gridSettings.LengthX.Value;

        else if (x < 0)
            x = _gridSettings.LengthX.Value - 1;

        if (y % _gridSettings.LengthY.Value == 0)
            y = y % _gridSettings.LengthY.Value;

        else if (y < 0)
            y = _gridSettings.LengthY.Value - 1;
    }

    public GridCell GetRandomEmptyGridCell()
    {
        int emptyCellsCount = _emptyCells.Count;

        int indexRNG = Random.Range(0, emptyCellsCount);


        return _emptyCells[indexRNG];
    }

    public GridCell GetFarFromEntitiesGridCell()
    {
        List<Entity> snakeHeads = GetEverySnake();
        if (snakeHeads.Count == 0)
        {
            sideX = 1;
            lastY = 0;
            return GetGridCellByCoordinate(3, 0);
        }
        int x = 0;
        int y = ++lastY;

        sideX *= -1;
        if (sideX > 0)
            x = 3;
        if(sideX < 0)
            x = _gridSettings.LengthX.Value - 3;

        return GetGridCellByCoordinate(x, y);
    }

    private List<Entity> GetEverySnake()
    {
        List<Entity> entities = GetEntities();
        List<Entity> snakeHeads = new List<Entity>();
        foreach (Entity entity in entities)
        {
            SnakeBlock snakeBlock = entity.GetComponent<SnakeBlock>();
            if (snakeBlock != null && snakeBlock.IsHead)
            {
                snakeHeads.Add(entity);
            }
        }
        return snakeHeads;
    }

    public List<Entity> GetEntities()
    {
        List<Entity> entities = new List<Entity>();

        foreach (GridCell gridCell in _gridCells)
        {
            if (gridCell.ocupated)
            {
                entities.Add(gridCell.entityOcupating);
            }
        }
        return entities;
    }


    public void ClearGrid()
    {
        foreach (Entity entity in GetEntities())
        {
            entity.currentGridCell.RemoveEntityFromThisCell();
            Destroy(entity.gameObject);
        }
    }
}
