using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TupleEvent", menuName = "GameEvents/TupleEvent")]
public class TupleEvent : BaseGameEvent<Tuple<int, GameObject>>
{
}