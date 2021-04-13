using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [SerializeField] private FloatVariable _movementsPerSecond;
    [SerializeField] private IntVariable _startX;
    [SerializeField] private IntVariable _startY;

    [SerializeField] private FloatVariable _startScale;
    [SerializeField] private FloatVariable _feedScale;

    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private bool _isAI;

    public FloatVariable MovementsPerSecond { get { return _movementsPerSecond; } }
    public IntVariable StartX { get { return _startX; } }
    public IntVariable StartY { get { return _startY; } }
    public FloatVariable StartScale { get { return _startScale; } }
    public FloatVariable FeedScale { get { return _feedScale; } }
    public GameObject TailPrefab { get { return _tailPrefab; } }
    public bool IsAI { get { return _isAI; } }
}
