using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum TypeOfEntity {Player, Enemy, Wall, Food};

    [SerializeField]
    private TypeOfEntity _typeOfEntity;
    public TypeOfEntity typeOfEntity => _typeOfEntity;
    

    private GridCell _currentGridCell;

    public GridCell currentGridCell
    {
        set
        {
            if(_currentGridCell != null)
            {
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
