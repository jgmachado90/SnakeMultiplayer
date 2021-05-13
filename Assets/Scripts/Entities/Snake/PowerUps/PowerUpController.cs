using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private int _blocksQuantity;
    public int BlockQuantity { get { return _blocksQuantity; } set { _blocksQuantity = value; } }

    public virtual void CollectPowerUp()
    {
        BlockQuantity++;
    }

    public virtual void SpentPowerUp(int quantityToSpent)
    {
        BlockQuantity -= quantityToSpent;
    }

    public virtual void ClearPowerUp()
    {
        BlockQuantity = 0;
    }
    

}
