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
        collector.GetComponentInParent<EnginePowerUpController>().ActivateEnginePowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
