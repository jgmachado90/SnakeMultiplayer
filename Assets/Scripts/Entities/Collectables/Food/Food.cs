using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Entity, ICollectable
{
    public VoidEvent OnCollectFood;

    private void Start()
    {
    
    }
    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(currentGridCell, collector);
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
