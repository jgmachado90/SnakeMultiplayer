using UnityEngine;
public interface ICollectable 
{
    void Collect(Entity collector);

    CollectableType CollectableType { get; set; }

}

public enum CollectableType
{
    Food,
    BatteringRam,
    EnginePower,
    TimeTravel
}
