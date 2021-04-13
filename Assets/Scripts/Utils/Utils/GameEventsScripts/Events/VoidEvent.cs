using UnityEngine;

[CreateAssetMenu(fileName = "NewVoidEvent", menuName = "GameEvents/VoidEvent")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}