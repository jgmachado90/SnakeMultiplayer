using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPowerUpController : MonoBehaviour
{
    [SerializeField] private GridInstance _gridInstance;
    [SerializeField] private GridInfo _gridInfo;

    [SerializeField] private GridInstanceEvent _OnTimeTravel;


    private void Start()
    {
        _gridInfo._gridCells.Clear();
        _gridInfo._emptyCells.Clear();
        //_gridInstance.EntitiesInfo.Clear();
    }

    public void OnCollectTimeTravelPowerUp()
    {
        _gridInstance.SaveGridInstance();
    }

    public void ActivatePowerUp()
    {
        _OnTimeTravel.Raise(_gridInstance);
    }

    private void Update()
    {

    }
}
