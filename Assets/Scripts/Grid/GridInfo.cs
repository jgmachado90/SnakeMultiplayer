using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Info", fileName = "GridInfo")]
public class GridInfo : ScriptableObject
{
    [SerializeField] private GridSettings _gridSettings;

    public List<GridCell> _gridCells = new List<GridCell>();
    public List<GridCell> _emptyCells = new List<GridCell>();

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

        int indexRNG = UnityEngine.Random.Range(0, emptyCellsCount);


        return _emptyCells[indexRNG];
    }

    public List<Entity> GetEntities()
    {
        List<Entity> entities = new List<Entity>();

        foreach(GridCell gridCell in _gridCells)
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
        foreach(Entity entity in GetEntities())
        {
            Destroy(entity.gameObject);
        }
    }

}
