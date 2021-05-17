using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Settings", fileName = "FoodData")]
public class CollectableSettings : ScriptableObject
{
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private GameObject _enginePowerPrefab;
    [SerializeField] private GameObject _batteringRamPrefab;
    [SerializeField] private GameObject _timeTravelPrefab;

    public GameObject FoodPrefab { get { return _foodPrefab; } }
    public GameObject EnginePowerPrefab { get { return _enginePowerPrefab; } }

    public GameObject BatteringRamPrefab { get { return _batteringRamPrefab; } }

    public GameObject TimeTravelPrefab { get { return _timeTravelPrefab; } }

}
