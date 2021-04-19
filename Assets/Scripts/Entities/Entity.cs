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
            value.SetEntityInThisCell(this);
            _currentGridCell = value;
        }
        get
        {
            return _currentGridCell;
        }
    }

   
}
