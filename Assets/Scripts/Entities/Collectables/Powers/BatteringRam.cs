using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRam : CollectableBlocks
{
    private void Start()
    {
        CollectableType = CollectableType.BatteringRam;
    }
    public override void Collect(Entity collector)
    {
        base.Collect(collector);
        collector.GetComponentInParent<BatteringRamPowerUpController>().CollectPowerUp();
        OnCollectBlock.Raise();
        Destroy(this.gameObject);
    }
}
