using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory : MonoBehaviour
{

    [Header("STATIC DATA")]
    [SerializeField] SnakePrefabsData snakesPrefabs;
    [SerializeField] List<SnakeStartingBlocks> snakeStartingBlocks;

    [Header("DYNAMIC DATA")]
    [SerializeField] private PlayerInput _currentInput;
    public PlayerInput CurrentInput { get { return _currentInput; } }

    [Header("EVENTS")]
    [SerializeField] private VoidEvent OnInitializeFirstSnake;

    private SnakeInitializer lastSnakeCreated;
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

    public void AssignLastSnakeInputKeys()
    {
        lastSnakeCreated.AssignInputKeys(CurrentInput.MyInput.Item1, CurrentInput.MyInput.Item2);
    }


    public void PrepareToReceiveNewSnake()
    {
        OnInitializeFirstSnake.Raise();
        lastSnakeCreated = null;
        currentSnakeStartingBlocksIndex = 0;
    }

}
