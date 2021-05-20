using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : CollectableBlocks
{
    public VoidEvent OnCollectEnginePower;

    private void Start()
    {
        CollectableType = CollectableType.EnginePower;
    }

    public override void Collect(Entity collector)
    {
        base.Collect(collector);
        collector.GetComponentInParent<EnginePowerUpController>().CollectPowerUp();
        OnCollectBlock.Raise();
        Destroy(this.gameObject);
    }
}
