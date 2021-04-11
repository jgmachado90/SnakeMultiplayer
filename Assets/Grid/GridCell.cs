using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCell : MonoBehaviour
{
    public GridCellCoordinates coordinate = new GridCellCoordinates();

    public bool ocupated;

    public Entity entityOcupating;

    public Action<GridCell> OnAddEntity;
    public Action<GridCell> OnRemoveEntity;

    public void SetEntityInThisCell(Entity entity)
    {
        ocupated = true;
        entityOcupating = entity;
        entity.transform.position = new Vector3(coordinate.x, coordinate.y, 0);
        OnAddEntity?.Invoke(this);

    }

    public void RemoveEntityFromThisCell()
    {
        ocupated = false;
        entityOcupating = null;
        OnRemoveEntity?.Invoke(this);
    }

}
