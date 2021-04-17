using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRamPowerUpController : MonoBehaviour
{
    [SerializeField] private SnakeSettings _snakeSettings;
    public void ActivateBatteringRamPowerUp()
    {
        _snakeSettings.HasBatteringRam = true;
    }
    public void DeactivateBatteringRamPowerUp()
    {
        _snakeSettings.HasBatteringRam = false;
    }
}
