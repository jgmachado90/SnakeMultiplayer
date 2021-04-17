using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : Entity, ICollectable
{
    public VoidEvent OnCollectFood;

    public void Collect(Entity collector)
    {
        collector.GetComponentInParent<Snake>().Feed(currentGridCell, collector);
        collector.GetComponentInParent<TimeTravelPowerUpController>().OnCollectTimeTravelPowerUp();
        OnCollectFood.Raise();
        Destroy(this.gameObject);
    }
}
