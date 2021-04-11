using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Entity, ICollectable
{

    private void Start()
    {
    
    }
    public void Collect(Entity collector)
    {
        collector.GetComponent<Snake>().Feed(currentGridCell);
        Destroy(this.gameObject);
    }
}
