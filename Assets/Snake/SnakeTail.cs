using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : Entity
{
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
