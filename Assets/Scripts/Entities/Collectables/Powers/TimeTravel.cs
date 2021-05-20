using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : CollectableBlocks
{
    private void Start()
    {
        CollectableType = CollectableType.TimeTravel;
    }
    public override void Collect(Entity collector)
    {
        base.Collect(collector);
        collector.GetComponentInParent<TimeTravelPowerUpController>().CollectPowerUp();
        OnCollectBlock.Raise();
        Destroy(this.gameObject);
    }
}
