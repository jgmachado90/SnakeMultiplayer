using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Settings", fileName = "GridData")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private int _lengthX;
    [SerializeField] private int _lengthY;
    [SerializeField] private GameObject _gridPrefab;

    public int LengthX { get { return _lengthX; } }
    public int LengthY { get { return _lengthX; } }

    public GameObject GridPrefab { get { return _gridPrefab; } }

}
