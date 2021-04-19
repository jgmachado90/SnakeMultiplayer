using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Entity, ICollectable
{
    public CollectableType CollectableType { get; set; }
    public VoidEvent OnCollectFood;

    private void Start()
    {
        CollectableType = CollectableType.Food;
    }
    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(collector, CollectableType);
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
