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

    [SerializeField] private CollectableType _firstBlock;
    [SerializeField] private CollectableType _secondBlock;
    [SerializeField] private CollectableType _thirdBlock;
                
    public SnakeMovementSettings SnakeMovementSettings { get { return _snakeMovementSettings; } }
    public SnakeAestheticsSettings SnakeAestheticsSettings { get { return _snakeAestheticsSettings; } }
    public SnakeInGameSettings SnakeInGameSettings { get { return _snakeInGameSettings; } }
    public SnakePrefabsSettings SnakePrefabSettings { get { return _snakePrefabSettings; } }

    public CollectableType FirstBlock { get { return _firstBlock; } }
    public CollectableType SecondBlock { get { return _secondBlock; } }
    public CollectableType ThirdBlock { get { return _thirdBlock; } }


}
