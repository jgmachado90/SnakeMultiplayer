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
        if (x == _gridSettings.LengthX.Value)
            x = x - _gridSettings.LengthX.Value;
        else if (x == -1)
            x = _gridSettings.LengthX.Value-1;

        if(y == _gridSettings.LengthY.Value)
            y = y - _gridSettings.LengthY.Value;

        else if (y == -1)
            y = _gridSettings.LengthY.Value - 1;

        return _gridCells[(_gridSettings.LengthX.Value * y) + x];
    }

    public GridCell GetRandomEmptyGridCell()
    {
        int emptyCellsCount = _emptyCells.Count;

        int indexRNG = UnityEngine.Random.Range(0, emptyCellsCount);


        return _emptyCells[indexRNG];
    }

}
