using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Snake/Settings", fileName = "SnakeData")]
public class SnakeSettings : ScriptableObject
{
    [Header("SnakeSettings References")]
    [SerializeField] private SnakeMovementSettings _snakeMovementSettings;
    [SerializeField] private SnakeScaleSettings _snakeScaleSettings;
    [SerializeField] private SnakePrefabsData _snakePrefabsData;
   
    public SnakeMovementSettings SnakeMovementSettings { get { return _snakeMovementSettings; } }
    public SnakeScaleSettings SnakeScaleSettings { get { return _snakeScaleSettings; } }
    public SnakePrefabsData SnakePrefabsData { get { return _snakePrefabsData; } }
}
