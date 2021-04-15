using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Settings", fileName = "FoodData")]
public class CollectableSettings : ScriptableObject
{
    [SerializeField] private float _timeToSpawnNewCollectable;
    [SerializeField] private int _startingCollectableCount;

    [SerializeField] private GameObject _foodPrefab;

    [SerializeField] private GameObject _enginePowerPrefab;



    public float TimeToSpawnNewCollectable { get { return _timeToSpawnNewCollectable; } }
    public int StartingCollectableCount { get { return _startingCollectableCount; } }
    public GameObject FoodPrefab { get { return _foodPrefab; } }
    public GameObject EnginePowerPrefab { get { return _enginePowerPrefab; } }

}
