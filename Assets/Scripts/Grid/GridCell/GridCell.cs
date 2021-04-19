using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCell : MonoBehaviour
{

    public GridCellCoordinates coordinate;// = new GridCellCoordinates();
    public bool ocupated;
    public Entity entityOcupating;
 

    [SerializeField] private GridManager _gridManager;

    public void SetEntityInThisCell(Entity entity)
    {
        if(entity.currentGridCell != null && entity.currentGridCell.entityOcupating == entity) {
            entity.currentGridCell.RemoveEntityFromThisCell();
        }
        
        ocupated = true;
        entityOcupating = entity;
        entity.transform.position = new Vector3(coordinate.x, coordinate.y, 0);
        _gridManager.RemoveCellFromEmptyList(this);
     
    }

    public void RemoveEntityFromThisCell()
    {
        ocupated = false;
        entityOcupating = null;
        _gridManager.AddCellInEmptyList(this);
    }

}
                       