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
            Debug.Log("starting New Snake");
            InstantiateNewSnake(KeyCode.A, KeyCode.D);

        }
    }

    public void InstantiateNewSnake(KeyCode keyLeft, KeyCode KeyRight)
    {
        GameObject newSnakeGameObject = Instantiate(snakePrefabs.SnakePrefab);
        Snake newSnake = newSnakeGameObject.GetComponent<Snake>();

        newSnake.StartSnake(keyLeft, KeyRight);
        
    }
}
