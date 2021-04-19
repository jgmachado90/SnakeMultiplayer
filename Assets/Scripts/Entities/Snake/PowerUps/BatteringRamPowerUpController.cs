using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRamPowerUpController : MonoBehaviour
{
    [SerializeField] private SnakeInGameSettings _snakeInGameSettings;
    public void CollectBatteringRamPowerUp()
    {
        _snakeInGameSettings.BatteringRam += 1;
        
        _snakeInGameSettings.CurrentHead.BatteringRam.gameObject.SetActive(true);
    }
    public void RemovingBatteringRamPowerUp()
    {
        if (_snakeInGameSettings.BatteringRam > 0)
        {
            List<SnakeBlock> snake = _snakeInGameSettings.Snake;
            foreach(SnakeBlock snakeBlock in snake)
            {
                if(snakeBlock.BlockType == CollectableType.BatteringRam)
                {
                    snakeBlock.BlockType = CollectableType.Food;
                    _snakeInGameSettings.BatteringRam -= 1;
                    if (_snakeInGameSettings.BatteringRam == 0)
                        _snakeInGameSettings.CurrentHead.BatteringRam.gameObject.SetActive(false);
                  
                    break;
                }
            }
        }
           
    }


}
