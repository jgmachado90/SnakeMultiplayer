using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlocks : Entity, ICollectable
{
    [Header("EVENTS")]
    [SerializeField] protected VoidEvent OnCollectBlock;

    
    public CollectableType CollectableType { get; set; }

    public virtual void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(collector, CollectableType);
    }
}
