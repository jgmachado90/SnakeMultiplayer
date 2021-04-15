using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridInfo _gridInfo;

    [Header("Food")]
    [SerializeField] private CollectableSettings _collectableSettings;

    public void InstantiateNewFood()
    {
        if(UnityEngine.Random.Range(0,2) < 1)
        {
            InstantiateFood(_collectableSettings.FoodPrefab);
        }
        else
        {
            InstantiateFood(_collectableSettings.EnginePowerPrefab);
        }

        
    }

    private void InstantiateFood(GameObject collectable)
    {
        GameObject foodGO = Instantiate(collectable);
        Entity newfood = foodGO.GetComponent<Entity>();
        newfood.currentGridCell = _gridInfo.GetRandomEmptyGridCell();
    }

    public void InstantiateFirstFood()
    {
        InstantiateNewFood();
    }

}
