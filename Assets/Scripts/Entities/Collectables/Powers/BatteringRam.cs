using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRam : Entity, ICollectable
{
    public VoidEvent OnCollectFood;

    public CollectableType CollectableType { get; set; }

    private void Start()
    {
        CollectableType = CollectableType.BatteringRam;
    }
    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(collector, CollectableType);
        collector.GetComponentInParent<BatteringRamPowerUpController>().CollectBatteringRamPowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
