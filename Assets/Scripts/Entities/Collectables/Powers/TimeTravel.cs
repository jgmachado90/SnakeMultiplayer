using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : Entity, ICollectable
{
    public VoidEvent OnCollectFood;
    public CollectableType CollectableType { get; set; }

    private void Start()
    {
 
    }
    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(collector, CollectableType);
        collector.GetComponentInParent<TimeTravelPowerUpController>().OnCollectTimeTravelPowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
