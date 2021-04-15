using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : Entity, ICollectable
{
    public VoidEvent OnCollectEnginePower;
    public VoidEvent OnCollectFood;

    public void Collect(Entity collector)
    {
        collector.GetComponent<Snake>().Feed(currentGridCell);
        OnCollectEnginePower.Raise();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
