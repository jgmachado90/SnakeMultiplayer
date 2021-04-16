using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Instance", fileName = "GridInstance")]
public class GridInstance : ScriptableObject
{
    [SerializeField] private GridInfo _gridInfo;
    [SerializeField] private List<Entity> _entitiesInGrid = new List<Entity>();
    [SerializeField] private List<GridCell> _entitiesGridCell = new List<GridCell>();

    public List<Entity> EntitiesInGrid { get { return _entitiesInGrid; } }
    public List<GridCell> EntitiesGridCell { get { return _entitiesGridCell; } }
    public void SaveGridInstance()
    {
        _entitiesInGrid.Clear();
        _entitiesGridCell.Clear();
        List<Entity> entities = _gridInfo.GetEntities();
        foreach(Entity e in entities)
        {
            _entitiesInGrid.Add(e);
            _entitiesGridCell.Add(e.currentGridCell);
        }
    }

    public void LoadGridInstance()
    {

    }


}
