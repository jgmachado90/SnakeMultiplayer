using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakesInputHandeler : MonoBehaviour
{

    public SnakeFactory snakeFactory;

    public InputHandeler inputHandeler;

    public List<Tuple<KeyCode, KeyCode>> playerInputKeys = new List<Tuple<KeyCode, KeyCode>>();

    private Tuple<KeyCode, KeyCode> _currentInputKeys;



    float time;
    private float currentTimeToAssignNewPlayer;
    private float firstTimeToAssignNewPlayer;
    public float timePressedToAssignNewPlayer;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if (inputHandeler.activeInputs.Count == 2 && _currentInputKeys == null)
        {
            SetCurrentInputKeys();
            currentTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
            firstTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
            time = Time.time;
        }

        else if (inputHandeler.activeInputs.Count == 2 && _currentInputKeys != null)
        {
            if (Time.time >= currentTimeToAssignNewPlayer)
            {
                if (IsNewInputKeys())
                    CreateNewSnake();
                else
                    snakeFactory.ChangeSnakePrefab();

                currentTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
            }
        }

        if (inputHandeler.activeInputs.Count < 2 && _currentInputKeys != null)
        {
            if (Time.time >= firstTimeToAssignNewPlayer)
            {
                snakeFactory.AssignLastSnakeInputKeys(_currentInputKeys.Item1, _currentInputKeys.Item2);
                snakeFactory.PrepareToReceiveNewSnake();
                _currentInputKeys = null;
            }
            else
            {
                _currentInputKeys = null;
            }
        }
    }

    private bool IsNewInputKeys()
    {
        if (playerInputKeys.Contains(_currentInputKeys))
            return false;
        return true;
    }

    private void CreateNewSnake()
    {
        snakeFactory.InstantiateNewSnake();
        playerInputKeys.Add(_currentInputKeys);
        //_currentInputKeys = null;
    }

    private void SetCurrentInputKeys()
    {
        if (_currentInputKeys == null)
        {
            KeyCode firstKey = inputHandeler.activeInputs[0];
            KeyCode secondKey = inputHandeler.activeInputs[1];
            _currentInputKeys = new Tuple<KeyCode, KeyCode>(firstKey, secondKey);
        }
    }
}
