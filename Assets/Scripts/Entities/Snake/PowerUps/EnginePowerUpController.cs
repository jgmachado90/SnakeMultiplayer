using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePowerUpController : PowerUpController
{
    [SerializeField] private SnakeMovementSettings _snakeMovementSettings;
    [SerializeField] private float _snakeBuff;

    private void Start()
    {
        _snakeMovementSettings.SpeedBuff.Value = 0;
    }
    public override void CollectPowerUp()
    {
        base.CollectPowerUp();
        _snakeMovementSettings.SpeedBuff.Value += _snakeBuff;
    }

    public override void ClearPowerUp()
    {
        base.ClearPowerUp();
        _snakeMovementSettings.SpeedBuff.Value = 0;
    }

}
