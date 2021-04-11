using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Settings", fileName = "FoodData")]
public class FoodSettings : ScriptableObject
{
    [SerializeField] private float _timeToSpawnNewFood;
    [SerializeField] private int _startingFoodCount;
    [SerializeField] private GameObject _foodPrefab;

    public float TimeToSpawnNewFood { get { return _timeToSpawnNewFood; } }
    public int StartingFoodCount { get { return _startingFoodCount; } }
    public GameObject FoodPrefab { get { return _foodPrefab; } }

}
