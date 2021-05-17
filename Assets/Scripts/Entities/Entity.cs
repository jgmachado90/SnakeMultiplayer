using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            value.SetEntityInThisCell(this);
            _currentGridCell = value;
        }
        get
        {
            return _currentGridCell;
        }
    }

    [SerializeField] private TupleEvent OnSendReference;

    public void SendReference(int id)
    {
        Tuple<int, GameObject> reference = new Tuple<int, GameObject>(id, gameObject);
        OnSendReference.Raise(reference);
    }

    [SerializeField] private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } set { _prefab = value; } }

    public void DeleteThisEntity()
    {
        //currentGridCell = null;
        Destroy(gameObject);
    }
   
}
