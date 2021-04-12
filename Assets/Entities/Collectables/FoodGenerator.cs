using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public static FoodGenerator instance;

    [SerializeField] private FoodSettings _foodSettings;

    private void Awake()
    {
        instance = this;
        GridGenerator.instance.OnCreateGrid += InstantiateNewFood;
    }

    public void InstantiateNewFood()
    {
        GameObject foodGO = Instantiate(_foodSettings.FoodPrefab);
        Food newfood = foodGO.GetComponent<Food>();
        newfood.currentGridCell = GridGenerator.instance.GetRandomEmptyGridCell();
    }

    

 

}
