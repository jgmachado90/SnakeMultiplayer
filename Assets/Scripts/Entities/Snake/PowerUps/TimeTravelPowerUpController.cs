using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPowerUpController : PowerUpController
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObjectEvent OnGetTimeTravelBlock;
    [SerializeField] private GameObjectEvent OnSnakeTimeTravel;

    public override void CollectPowerUp()
    {
        base.CollectPowerUp();
        OnGetTimeTravelBlock.Raise(gameObject);
    }

    public override void ClearPowerUp()
    {
        base.ClearPowerUp();
    }

    public void OnTimeTravel()
    {
        Snake snake = GetComponent<Snake>();
        snake.ThisSnake.Clear();
    }

    public void StartTimeTravel()
    {
        OnSnakeTimeTravel.Raise(gameObject);
    }
}
