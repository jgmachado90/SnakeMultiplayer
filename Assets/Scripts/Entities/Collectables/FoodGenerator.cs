using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridInfo _gridInfo;

    [Header("Food")]
    [SerializeField] private FoodSettings _foodSettings;

    public void InstantiateNewFood()
    {
        GameObject foodGO = Instantiate(_foodSettings.FoodPrefab);
        Food newfood = foodGO.GetComponent<Food>();
        newfood.currentGridCell = _gridInfo.GetRandomEmptyGridCell();
    }

    public void InstantiateFirstFood()
    {
        InstantiateNewFood();
    }

}
