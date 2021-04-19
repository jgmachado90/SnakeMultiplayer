using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/Aesthetics", fileName = "SnakeAestheticsSettings")]
public class SnakeAestheticsSettings : ScriptableObject
{
    [SerializeField] private FloatVariable _startScale;
    [SerializeField] private FloatVariable _feedScale;

    public FloatVariable StartScale { get { return _startScale; } }
    public FloatVariable FeedScale { get { return _feedScale; } }
}
