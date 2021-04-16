using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPowerUpController : MonoBehaviour
{
    [SerializeField] private GridInstance _gridInstance;
    [SerializeField] private GridInfo _gridInfo;

    [SerializeField] private VoidEvent _OnTimeTravel;


    private void Start()
    {
        _gridInstance.EntitiesInGrid.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _gridInstance.SaveGridInstance();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _OnTimeTravel.Raise();
        }




    }
}
