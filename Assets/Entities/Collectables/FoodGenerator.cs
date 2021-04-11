using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public static FoodGenerator instance;

    private Coroutine _foodGenerationCoroutine;
    [SerializeField] private FoodSettings _foodSettings;

    private void Awake()
    {
        instance = this;
    }

    public void instantiateNewFood()
    {
        GameObject foodGO = Instantiate(_foodSettings.FoodPrefab);
        Food newfood = foodGO.GetComponent<Food>();
        newfood.currentGridCell = GridGenerator.instance.GetRandomEmptyGridCell();

    }

    private void Start()
    {
        _foodGenerationCoroutine = StartCoroutine(FoodGenerationCoroutine());

    }

    private IEnumerator FoodGenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_foodSettings.TimeToSpawnNewFood);
            instantiateNewFood();
        }

    }

}
