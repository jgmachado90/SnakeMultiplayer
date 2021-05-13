using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/ScaleSettings", fileName = "SnakeScaleSettings")]
public class SnakeScaleSettings : ScriptableObject
{
    [SerializeField] private FloatVariable _startScale;
    [SerializeField] private FloatVariable _feededScale;
    public FloatVariable StartScale { get { return _startScale; } }
    public FloatVariable FeededScale { get { return _feededScale; } }
}
