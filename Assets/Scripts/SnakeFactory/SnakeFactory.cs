using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory : MonoBehaviour
{
    [SerializeField] SnakePrefabsSettings snakePrefabs;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject newSnakeGameObject = Instantiate(snakePrefabs.SnakePrefab);
            newSnakeGameObject.GetComponent<Snake>().StartSnake();

        }
    }
}
