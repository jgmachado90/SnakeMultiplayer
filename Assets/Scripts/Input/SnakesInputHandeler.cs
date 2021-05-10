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
    public float timePressedToAssignNewPlayer;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if (inputHandeler.activeInputs.Count == 2 && _currentInputKeys == null)
        {
            AssignKeys();
            time = Time.time;
        }
      

        if (inputHandeler.activeInputs.Count < 2 && _currentInputKeys != null)
        {
            if (Time.time >= time + timePressedToAssignNewPlayer)
            {
                if (IsNewInputKeys())
                    CreateNewPlayer();
            }
            else
                _currentInputKeys = null;
        }
    }

    private bool IsNewInputKeys()
    {
        bool isNewPlayer = true;
        foreach (Tuple<KeyCode, KeyCode> playerKeys in playerInputKeys)
            isNewPlayer = IsNewPlayerInput(isNewPlayer, playerKeys);
        return isNewPlayer;
    }

    private void CreateNewPlayer()
    {
        snakeFactory.InstantiateNewSnake(_currentInputKeys.Item1, _currentInputKeys.Item2);
        playerInputKeys.Add(_currentInputKeys);
        _currentInputKeys = null;
    }

    private bool IsNewPlayerInput(bool isNewPlayer, Tuple<KeyCode, KeyCode> playerKeys)
    {
        if (_currentInputKeys.Item1 == playerKeys.Item1 && _currentInputKeys.Item2 == playerKeys.Item2)
        {
            isNewPlayer = false;
            _currentInputKeys = null;
        }
        return isNewPlayer;
    }

    private void AssignKeys()
    {
        if (_currentInputKeys == null)
        {
            KeyCode firstKey = inputHandeler.activeInputs[0];
            KeyCode secondKey = inputHandeler.activeInputs[1];
            _currentInputKeys = new Tuple<KeyCode, KeyCode>(firstKey, secondKey);
        }
    }
}
