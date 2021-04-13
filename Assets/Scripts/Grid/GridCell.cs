using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCell : MonoBehaviour
{
    public GridCellCoordinates coordinate = new GridCellCoordinates();

    public bool ocupated;

    public Entity entityOcupating;

    [SerializeField] private GridInfo _gridInfo;

  

    public void SetEntityInThisCell(Entity entity)
    {
        ocupated = true;
        entityOcupating = entity;
        entity.transform.position = new Vector3(coordinate.x, coordinate.y, 0);
        _gridInfo.RemoveCellFromEmptyList(this);
    }

    public void RemoveEntityFromThisCell()
    {
        ocupated = false;
        entityOcupating = null;
        _gridInfo.AddCellInEmptyList(this);
    }

}
