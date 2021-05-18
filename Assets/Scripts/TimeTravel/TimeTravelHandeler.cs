using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelHandeler : MonoBehaviour
{
    List<TimeTravelData> snakesTimeTravelData = new List<TimeTravelData>();

    int timeTravelId;
    [SerializeField] private IntEvent OnRequestReferences;
    [SerializeField] private VoidEvent OnTimeTravel;
    [SerializeField] private VoidEvent OnTimeTravelComplete;

    [SerializeField] private CollectableSettings collectableSettings;
    [SerializeField] private SnakePrefabsSettings snakePrefabsSettings;

    private void Start()
    {
        timeTravelId = 0;
    }

    //When a snake gets a time travel block, it will send an event to time 
    //travel handeler, in order to record the grid state.
    public void OnSnakeGetTimeTravelBlock(GameObject snakeGO)
    {
        Snake snake = snakeGO.GetComponent<Snake>();
        RemoveOldTimeTravelData(snake);

        TimeTravelData newTimeTravelData = new TimeTravelData(timeTravelId, snake);
        snakesTimeTravelData.Add(newTimeTravelData);

        OnRequestReferences.Raise(timeTravelId);
        timeTravelId++;
    }

    private void RemoveOldTimeTravelData(Snake snake)
    {
        TimeTravelData thisTimeTravel = null;
        foreach (TimeTravelData timeTravelData in snakesTimeTravelData)
        {
            if (timeTravelData.mainSnake == snake)
            {
                thisTimeTravel = timeTravelData;
            }
        }
        snakesTimeTravelData.Remove(thisTimeTravel);
    }

    //After request references, every snake or collectable will answer back sending their referencies
    public void OnGetReference(Tuple<int, GameObject> reference)
    {
        int id = reference.Item1;
        GameObject referenceGO = reference.Item2;

        foreach (TimeTravelData timeTravelData in snakesTimeTravelData)
        {
            if (timeTravelData.id == id)
            {
                Entity entity = referenceGO.GetComponent<Entity>();
                SnakeBlock snakeBlock = entity.GetComponent<SnakeBlock>();
                if (snakeBlock != null)
                {
                    if (snakeBlock.IsHead)
                    {
                        Snake thisHeadSnake = snakeBlock.GetComponentInParent<Snake>();
                        timeTravelData.AddSnake(thisHeadSnake);
                    }
                }
                else if (entity is ICollectable)
                {
                    timeTravelData.AddFood(entity);
                }
            }
        }
    }
    
    public void OnStartTimeTravel(GameObject snakeGO)
    {
        Snake snake = snakeGO.GetComponent<Snake>();

        TimeTravelData thisTimeTravel = null;

        OnTimeTravel.Raise();

        foreach (TimeTravelData timeTravelData in snakesTimeTravelData)
        {
            if (timeTravelData.mainSnake == snake)
            {
                thisTimeTravel = timeTravelData;
                LoadSnakes(timeTravelData);
                LoadFoods(timeTravelData);
                OnTimeTravelComplete.Raise();
            }
        }
        snakesTimeTravelData.Remove(thisTimeTravel);
    }

    private void LoadFoods(TimeTravelData timeTravelData)
    {
        foreach(FoodData food in timeTravelData.foods)
        {
            GameObject newFoodGO = Instantiate(food.prefab, food.parent);
            Entity newFood = newFoodGO.GetComponent<Entity>();

            newFood.currentGridCell = food.entityCell;
        }
    }

    private void LoadSnakes(TimeTravelData timeTravelData)
    {
        Debug.Log("Time travel Load Snakes");
        foreach (var snake in timeTravelData.snakes){

            List<SnakeBlock> newThisSnake = new List<SnakeBlock>();
            foreach(SnakeTailData snakeTail in snake.Value)
            {
                GameObject newSnakeTailGO = Instantiate(snakePrefabsSettings.TailPrefab, snakeTail.parent);
                SnakeBlock newSnakeTail = newSnakeTailGO.GetComponent<SnakeBlock>();

                newSnakeTail.currentGridCell = snakeTail.entityCell;
                newSnakeTail.BlockType = snakeTail.type;
                newSnakeTail.IsHead = snakeTail.isHead;
                if (newSnakeTail.IsHead)
                {
                    newSnakeTail.spriteRenderer.sprite = newSnakeTail.headSprite;
                    snake.Key.CurrentHead = newSnakeTail;
                }
                newSnakeTail.spriteRenderer.color = snakeTail.color;

                newThisSnake.Add(newSnakeTail);
            }
            snake.Key.ThisSnake = newThisSnake;
            
        }
    }
}
