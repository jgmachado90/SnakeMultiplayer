using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : Entity, ICollectable
{
    public VoidEvent OnCollectEnginePower;
    public VoidEvent OnCollectFood;
    public CollectableType CollectableType { get; set; }

    private void Start()
    {
        CollectableType = CollectableType.EnginePower;
    }

    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(collector, CollectableType);
        collector.GetComponentInParent<EnginePowerUpController>().ActivateEnginePowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
