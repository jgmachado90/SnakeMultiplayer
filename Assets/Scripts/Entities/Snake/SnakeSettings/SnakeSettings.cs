using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [Header("SnakeSettings References")]
    [SerializeField] private SnakeMovementSettings _snakeMovementSettings;
    [SerializeField] private SnakeAestheticsSettings _snakeAestheticsSettings;
    [SerializeField] private SnakeInGameSettings _snakeInGameSettings;
    [SerializeField] private SnakePrefabsSettings _snakePrefabSettings;

    
    [SerializeField] private bool _isPlayer2;

    public SnakeMovementSettings SnakeMovementSettings { get { return _snakeMovementSettings; } }
    public SnakeAestheticsSettings SnakeAestheticsSettings { get { return _snakeAestheticsSettings; } }
    public SnakeInGameSettings SnakeInGameSettings { get { return _snakeInGameSettings; } }
    public SnakePrefabsSettings SnakePrefabSettings { get { return _snakePrefabSettings; } }

    public bool IsPlayer2 { get { return _isPlayer2; } }

}
