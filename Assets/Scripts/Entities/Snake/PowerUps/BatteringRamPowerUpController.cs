using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRamPowerUpController : MonoBehaviour
{
    [SerializeField] private int _batteringRam;
    [SerializeField] private Snake _snake;
    public int BatteringRam { get { return _batteringRam; } set { _batteringRam = value; } }

    public void CollectBatteringRamPowerUp()
    {
        BatteringRam += 1;

        _snake.CurrentHead.BatteringRam.gameObject.SetActive(true);
    }
    public void RemovingBatteringRamPowerUp()
    {
        if (BatteringRam > 0)
        {
            List<SnakeBlock> snake = _snake.ThisSnake;
            foreach(SnakeBlock snakeBlock in snake)
            {
                if(snakeBlock.BlockType == CollectableType.BatteringRam)
                {
                    snakeBlock.BlockType = CollectableType.Food;
                    BatteringRam -= 1;
                    if (BatteringRam == 0)
                        _snake.CurrentHead.BatteringRam.gameObject.SetActive(false);
                  
                    break;
                }
            }
        }
    }


}
