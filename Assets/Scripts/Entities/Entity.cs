using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum TypeOfEntity {Player, Enemy, Wall, Food};

    [SerializeField]
    private TypeOfEntity _typeOfEntity;
    public TypeOfEntity typeOfEntity => _typeOfEntity;
    
    [SerializeField] private EntityInfo _entityInfo;
    public EntityInfo EntityInfo {get { return _entityInfo; } }

    private GridCell _currentGridCell;

    public GridCell currentGridCell
    {
        set
        {
            if (value == null) return;
            if(_currentGridCell != null)
            {
                if(currentGridCell.entityOcupating == this)
                    _currentGridCell.RemoveEntityFromThisCell();
            }
            _currentGridCell = value;
            _currentGridCell.SetEntityInThisCell(this);
        }
        get
        {
            return _currentGridCell;
        }
    }

   
}
