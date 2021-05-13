using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRamPowerUpController : PowerUpController
{
    [SerializeField] private Snake _snake;

    public override void CollectPowerUp()
    {
        base.CollectPowerUp();
        _snake.CurrentHead.BatteringRam.gameObject.SetActive(true);
    }


    public void RemovingBatteringRamPowerUp()
    {
        if (BlockQuantity > 0)
        {
            List<SnakeBlock> snake = _snake.ThisSnake;
            foreach(SnakeBlock snakeBlock in snake)
            {
                if(snakeBlock.BlockType == CollectableType.BatteringRam)
                {
                    snakeBlock.BlockType = CollectableType.Food;
                    SpentPowerUp(1);
                    if (BlockQuantity == 0)
                        _snake.CurrentHead.BatteringRam.gameObject.SetActive(false);
                  
                    break;
                }
            }
        }
    }

    public override void ClearPowerUp()
    {
        base.ClearPowerUp();
        _snake.CurrentHead.BatteringRam.gameObject.SetActive(false);
    }


}
