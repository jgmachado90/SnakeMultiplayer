using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/Movement", fileName = "SnakeMovementSettings")]
public class SnakeMovementSettings : ScriptableObject
{
    [SerializeField] private FloatVariable _movementsPerSecond;
    [SerializeField] private IntVariable _startX;
    [SerializeField] private IntVariable _startY;
    [SerializeField] private FloatVariable _speedDebuff;
    [SerializeField] private FloatVariable _speedBuff;
    [SerializeField] private FloatVariable _snakeMaxSpeed;


    public FloatVariable MovementsPerSecond { get { return _movementsPerSecond; } }
    public IntVariable StartX { get { return _startX; } }
    public IntVariable StartY { get { return _startY; } }
    public FloatVariable SpeedDebuff { get { return _speedDebuff; } }

    public FloatVariable SpeedBuff { get { return _speedBuff; } }
    public FloatVariable SnakeMaxSpeed { get { return _snakeMaxSpeed; } }

}
