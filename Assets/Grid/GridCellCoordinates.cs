using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridCellCoordinates
{
    private int _x;
    private int _y;

    public int x
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }

    public int y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
        }
    }
}