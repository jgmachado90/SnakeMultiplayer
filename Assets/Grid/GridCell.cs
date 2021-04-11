using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GridCellCoordinates coordinate = new GridCellCoordinates();

    public bool ocupated;

    public Entity entityOcupating;

    public void SetEntityInThisCell(Entity entity)
    {
        ocupated = true;
        entityOcupating = entity;
        entity.transform.position = new Vector3(coordinate.x, coordinate.y, 0);
    }

    public void RemoveEntityFromThisCell()
    {
        ocupated = false;
        entityOcupating = null;
    }

}
