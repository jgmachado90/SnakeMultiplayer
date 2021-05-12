using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory : MonoBehaviour
{
    [SerializeField] SnakePrefabsSettings snakesPrefabs;
    [SerializeField] List<SnakeStartingBlocks> snakeStartingBlocks;

    SnakeInitializer lastSnakeCreated;
    private int currentSnakeStartingBlocksIndex;
    private void Awake()
    {
        currentSnakeStartingBlocksIndex = 0;
    }

    public void InstantiateNewSnake()
    {
        GameObject newSnakeGameObject = Instantiate(snakesPrefabs.SnakePrefab[0]);
        SnakeInitializer snakeInitializer = newSnakeGameObject.GetComponent<SnakeInitializer>();
        snakeInitializer.InitializeSnake(snakeStartingBlocks[currentSnakeStartingBlocksIndex]);
        lastSnakeCreated = snakeInitializer;
    }

    public void ChangeSnakePrefab()
    {
        if (currentSnakeStartingBlocksIndex < snakeStartingBlocks.Count - 1)
            currentSnakeStartingBlocksIndex++;
        else
            currentSnakeStartingBlocksIndex = 0;

        lastSnakeCreated.ChangeSnake(snakeStartingBlocks[currentSnakeStartingBlocksIndex]);
    }

    public void AssignLastSnakeInputKeys(KeyCode keyLeft, KeyCode keyRight)
    {
        lastSnakeCreated.AssignInputKeys(keyLeft, keyRight);
    }


    public void PrepareToReceiveNewSnake()
    {
        lastSnakeCreated = null;
        currentSnakeStartingBlocksIndex = 0;
    }

}
