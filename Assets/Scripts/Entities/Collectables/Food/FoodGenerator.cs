using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;

    [Header("Food")]
    [SerializeField] private CollectableSettings _collectableSettings;


    public int startCollectableCount;

    private void Start()
    {
        startCollectableCount = 0;
    }

    public void IncreaseStartCollectableCount()
    {
        startCollectableCount++;
    }

    public void InstantiateNewFood()
    {
        int rNG = UnityEngine.Random.Range(0, 100);
        if (rNG < 50)
        {
            InstantiateFood(_collectableSettings.FoodPrefab);
        }
        else if(rNG < 70)
        {
            InstantiateFood(_collectableSettings.EnginePowerPrefab);
        }
        else if(rNG < 80)
        {
            InstantiateFood(_collectableSettings.BatteringRamPrefab);
        }
        else
        {
            InstantiateFood(_collectableSettings.TimeTravelPrefab);
        }
    }

    private void InstantiateFood(GameObject collectable)
    {
        GameObject foodGO = Instantiate(collectable);
        Entity newfood = foodGO.GetComponent<Entity>();
        newfood.currentGridCell = _gridManager.GetRandomEmptyGridCell();
    }

    public void InstantiateFirstFood()
    {
        for(int i = 0; i < startCollectableCount; i++)
            InstantiateNewFood();
    }

}
