using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePowerUpController : MonoBehaviour
{
    [SerializeField] private SnakeSettings _snakeSettings;
    [SerializeField] private FloatVariable _snakeBuff;

    private void Start()
    {
        _snakeSettings.SpeedBuff.Value = 0;
    }
    public void ActivateEnginePowerUp()
    {
        _snakeSettings.SpeedBuff.Value += _snakeBuff.Value;
    }

}
