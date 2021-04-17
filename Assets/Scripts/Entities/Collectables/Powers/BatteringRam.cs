using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRam : Entity, ICollectable
{
    public VoidEvent OnCollectFood;

    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(currentGridCell, collector);
        collector.GetComponentInParent<BatteringRamPowerUpController>().ActivateBatteringRamPowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
