using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePowerUpController : MonoBehaviour
{
    [SerializeField] private SnakeMovementSettings _snakeMovementSettings;
    [SerializeField] private FloatVariable _snakeBuff;

    private void Start()
    {
        _snakeMovementSettings.SpeedBuff.Value = 0;
    }
    public void ActivateEnginePowerUp()
    {
        _snakeMovementSettings.SpeedBuff.Value += _snakeBuff.Value;
    }

}
