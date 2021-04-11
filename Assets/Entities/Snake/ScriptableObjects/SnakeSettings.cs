using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [SerializeField] private float _movementsPerSecond;
    [SerializeField] private int _startX;
    [SerializeField] private int _startY;
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private bool _isAI;

    public float MovementsPerSecond { get { return _movementsPerSecond; } }
    public int StartX { get { return _startX; } }
    public int StartY { get { return _startY; } }
    public GameObject TailPrefab { get { return _tailPrefab; } }
    public bool IsAI { get { return _isAI; } }
}
