using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/InGame", fileName = "SnakeInGameSettings")]
public class SnakeInGameSettings : ScriptableObject
{

    [SerializeField] private int _batteringRam;
    [SerializeField] private SnakeBlock _currentHead;
    [SerializeField] private List<SnakeBlock> _snake;

    public SnakeBlock CurrentHead { get { return _currentHead; } set { _currentHead = value; } }
    public List<SnakeBlock> Snake { get { return _snake; } set { _snake = value; } }
    public int BatteringRam { get { return _batteringRam; } set { _batteringRam = value; } }

    public void Clear()
    {
        Snake.Clear();
        _batteringRam = 0;
    }
}
