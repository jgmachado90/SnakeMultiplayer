using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/GridData", fileName = "GridData")]
public class GridData : ScriptableObject
{
    [SerializeField] private IntVariable _lengthX;
    [SerializeField] private IntVariable _lengthY;
    [SerializeField] private GameObject _gridPrefab;

    public IntVariable LengthX { get { return _lengthX; } }
    public IntVariable LengthY { get { return _lengthY; } }

    public GameObject GridPrefab { get { return _gridPrefab; } }

}
