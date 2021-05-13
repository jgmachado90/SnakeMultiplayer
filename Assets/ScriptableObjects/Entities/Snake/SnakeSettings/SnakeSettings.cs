using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [Header("SnakeSettings References")]
    [SerializeField] private SnakeMovementSettings _snakeMovementSettings;
    [SerializeField] private SnakeScaleSettings _snakeAestheticsSettings;
    [SerializeField] private SnakePrefabsSettings _snakePrefabSettings;
   
    public SnakeMovementSettings SnakeMovementSettings { get { return _snakeMovementSettings; } }
    public SnakeScaleSettings SnakeAestheticsSettings { get { return _snakeAestheticsSettings; } }
    public SnakePrefabsSettings SnakePrefabSettings { get { return _snakePrefabSettings; } }
}
