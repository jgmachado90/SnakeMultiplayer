using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCell : MonoBehaviour
{

    public GridCellCoordinates coordinate;// = new GridCellCoordinates();
    public bool ocupated;

    private Entity _entityOcupating;
    public Entity EntityOcupating { get { return _entityOcupating; } set { _entityOcupating = value;}  }
 

    [SerializeField] private GridManager _gridManager;

    public void SetEntityInThisCell(Entity entity)
    {
        if(entity.currentGridCell != null && entity.currentGridCell._entityOcupating == entity) {
            entity.currentGridCell.RemoveEntityFromThisCell();
        }
        
        ocupated = true;
        _entityOcupating = entity;
        entity.transform.position = new Vector3(coordinate.x, coordinate.y, 0);
        _gridManager.RemoveCellFromEmptyList(this);
     
    }

    public void RemoveEntityFromThisCell()
    {
        ocupated = false;
        _entityOcupating = null;
        _gridManager.AddCellInEmptyList(this);
    }

}
                       