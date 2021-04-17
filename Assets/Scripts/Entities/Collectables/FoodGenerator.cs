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
        int rNG = UnityEngine.Random.Range(0, 10);
        if (rNG > 4)
        {
            InstantiateFood(_collectableSettings.FoodPrefab);
        }
        else if(rNG > 2 )
        {
            InstantiateFood(_collectableSettings.EnginePowerPrefab);
        }
        else
        {
            InstantiateFood(_collectableSettings.BatteringRamPrefab);
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
        for(int i = 0; i < _collectableSettings.StartingCollectableCount; i++)
            InstantiateNewFood();
    }

}
