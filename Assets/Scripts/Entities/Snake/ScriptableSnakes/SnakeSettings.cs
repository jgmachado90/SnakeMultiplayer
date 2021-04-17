using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private FloatVariable _movementsPerSecond;
    [SerializeField] private IntVariable _startX;
    [SerializeField] private IntVariable _startY;
    [SerializeField] private FloatVariable _speedDebuff;
    [SerializeField] private FloatVariable _speedBuff;
    [SerializeField] private FloatVariable _snakeMaxSpeed;


    [Header("Aesthetics")]
    [SerializeField] private FloatVariable _startScale;
    [SerializeField] private FloatVariable _feedScale;

    [Header("Prefabs")]
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private bool _isAI;


    [Header("InGame")]
    [SerializeField] private bool _hasBatteringRam;
    [SerializeField] private SnakePart _currentHead;
    [SerializeField] private List<SnakePart> _snake;

    public FloatVariable MovementsPerSecond { get { return _movementsPerSecond; } }
    public IntVariable StartX { get { return _startX; } }
    public IntVariable StartY { get { return _startY; } }
    public FloatVariable SpeedDebuff { get { return _speedDebuff; } }

    public FloatVariable SpeedBuff { get { return _speedBuff; } }
    public FloatVariable SnakeMaxSpeed { get { return _snakeMaxSpeed; } }


    public FloatVariable StartScale { get { return _startScale; } }
    public FloatVariable FeedScale { get { return _feedScale; } }
    public GameObject TailPrefab { get { return _tailPrefab; } }

    public SnakePart CurrentHead { get { return _currentHead; }set { _currentHead = value; } }
    public List<SnakePart> Snake { get { return _snake; } set { _snake = value; } }
    public bool IsAI { get { return _isAI; } }

    public bool HasBatteringRam { get { return _hasBatteringRam; } set { _hasBatteringRam = value; } }

    public void Clear() {

        Snake.Clear();
        HasBatteringRam = false;
    }
}
