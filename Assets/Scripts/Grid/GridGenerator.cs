using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("STATIC DATA")]
    [SerializeField] private GridData _gridSettings;

    [Header("DYNAMIC DATA")]
    [SerializeField] private GridManager _gridManager;

    [Header("EVENTS")]
    public VoidEvent OnCreateGrid;

    private void Start()
    {
        _gridManager._gridCells.Clear();
        _gridManager._emptyCells.Clear();
        GenerateScenery();
        OnCreateGrid.Raise();
    }

    private void GenerateScenery()
    {
        for(int j = 0; j < _gridSettings.LengthY.Value; j++)
        {
            for(int i = 0; i < _gridSettings.LengthX.Value; i++)
            {
                GameObject gridPrefab = Instantiate(_gridSettings.GridPrefab, transform);
                gridPrefab.transform.position = new Vector3(i,j,0);
                GridCell gridCell = gridPrefab.GetComponent<GridCell>();
                gridCell.coordinate.x = i;
                gridCell.coordinate.y = j;
                _gridManager._gridCells.Add(gridCell);
                _gridManager._emptyCells.Add(gridCell);
            }
        }
    }
}



