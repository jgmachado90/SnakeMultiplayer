using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPowerUpController : PowerUpController
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObjectEvent OnGetTimeTravelBlock;
    [SerializeField] private GameObjectEvent OnSnakeTimeTravel;

    private void Update()
    {
        Debug.Log("Time Travel Block Quantity = " + BlockQuantity);
    }

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
        SpentPowerUp(1);
        Snake snake = GetComponent<Snake>();
        snake.ThisSnake.Clear();
    }

    public void StartTimeTravel()
    {
        OnSnakeTimeTravel.Raise(gameObject);
    }
}
