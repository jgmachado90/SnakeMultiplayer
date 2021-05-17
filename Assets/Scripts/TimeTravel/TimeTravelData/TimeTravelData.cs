using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeTravelData
{
    public int id;
    public Snake mainSnake;

    public Dictionary<Snake, List<SnakeTailData>> snakes = new Dictionary<Snake, List<SnakeTailData>>();

    public List<FoodData> foods = new List<FoodData>();


    public TimeTravelData(int _id, Snake _mainSnake)
    {
        id = _id;
        mainSnake = _mainSnake;
        AddSnake(_mainSnake);
    }

    public void AddSnake(Snake _snake)
    {
        if (snakes.ContainsKey(_snake))
            return;

        List<SnakeTailData> snakeTails = GetSnakeTailData(_snake);
        snakes.Add(_snake, snakeTails);
    }

    public void AddFood(Entity food)
    {
        ICollectable newFood = (ICollectable)food;

        FoodData newFoodData = new FoodData();
        newFoodData.entityCell = food.currentGridCell;
        newFoodData.prefab = food.Prefab;
        newFoodData.parent = food.transform.parent;
    }

    private List<SnakeTailData> GetSnakeTailData(Snake _snake)
    {
        List<SnakeTailData> snakeTails = new List<SnakeTailData>();

        foreach(SnakeBlock snakeBlock in _snake.ThisSnake)
        {
            SnakeTailData newSnakeTailData = new SnakeTailData();

            newSnakeTailData.isHead = snakeBlock.IsHead;
            newSnakeTailData.entityCell = snakeBlock.currentGridCell;
            newSnakeTailData.type = snakeBlock.BlockType;
            newSnakeTailData.prefab = snakeBlock.Prefab;
            newSnakeTailData.parent = snakeBlock.transform.parent;
            newSnakeTailData.color = snakeBlock.spriteRenderer.color;

            snakeTails.Add(newSnakeTailData);
        }

        return snakeTails;
    }

}
