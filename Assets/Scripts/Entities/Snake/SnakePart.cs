using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : Entity
{
    public bool isHead;
    public bool hasFood;

    private Entity _prox;

    public Entity Prox
    {
        get
        {
            return _prox;
        }
        set
        {
            _prox = value;
        }
    }

}
