using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : CollectableBlocks
{
    private void Start()
    {
        CollectableType = CollectableType.Food;
    }
    public override void Collect(Entity collector)
    {
        base.Collect(collector);
        OnCollectBlock.Raise();
        Destroy(this.gameObject);
    }
}
