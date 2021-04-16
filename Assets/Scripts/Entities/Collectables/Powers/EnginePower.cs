using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : Entity, ICollectable
{
    public VoidEvent OnCollectEnginePower;
    public VoidEvent OnCollectFood;

    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(currentGridCell, collector);
        OnCollectEnginePower.Raise();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
