using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakesInputHandeler : MonoBehaviour
{
    [SerializeField] private PlayerInput _currentInput;
    public PlayerInput CurrentInput { get { return _currentInput; } set { _currentInput = value; } }

    public InputHandeler inputHandeler;

    public List<Tuple<KeyCode, KeyCode>> playerInputKeys = new List<Tuple<KeyCode, KeyCode>>();

    [SerializeField] private VoidEvent OnInstantiateNewSnake;
    [SerializeField] private VoidEvent OnChangeSnakePrefab;
    [SerializeField] private VoidEvent OnAssignInputKeys;
    [SerializeField] private VoidEvent OnPrepareToReceiveNewSnake;




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
        if (inputHandeler.activeInputs.Count == 2 && CurrentInput.MyInput == null)
        {
            SetCurrentInputKeys();
            StartPressingTimeCount();
        }

        else if (inputHandeler.activeInputs.Count == 2 && CurrentInput.MyInput != null)
        {
            if (Time.time >= currentTimeToAssignNewPlayer)
            {
                if (IsNewInputKeys())
                    CreateNewSnake();
                else
                    OnChangeSnakePrefab.Raise();

                currentTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
            }
        }

        if (inputHandeler.activeInputs.Count < 2 && CurrentInput.MyInput != null)
        {
            if (Time.time >= firstTimeToAssignNewPlayer)
            {
                OnAssignInputKeys.Raise();
                OnPrepareToReceiveNewSnake.Raise();
             // snakeFactory.AssignLastSnakeInputKeys(_currentInputKeys.Item1, _currentInputKeys.Item2);
             // snakeFactory.PrepareToReceiveNewSnake();
                CurrentInput.MyInput = null;
            }
            else
            {
                CurrentInput.MyInput = null;
            }
        }
    }

    private void StartPressingTimeCount()
    {
        currentTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
        firstTimeToAssignNewPlayer = Time.time + timePressedToAssignNewPlayer;
        time = Time.time;
    }

    private bool IsNewInputKeys()
    {
        if (playerInputKeys.Contains(CurrentInput.MyInput))
            return false;
        return true;
    }

    private void CreateNewSnake()
    {
        OnInstantiateNewSnake.Raise();
        playerInputKeys.Add(CurrentInput.MyInput);
   
    }

    private void SetCurrentInputKeys()
    {
        if (CurrentInput.MyInput == null)
        {
            KeyCode firstKey = inputHandeler.activeInputs[0];
            KeyCode secondKey = inputHandeler.activeInputs[1];
            CurrentInput.MyInput = new Tuple<KeyCode, KeyCode>(firstKey, secondKey);
        }
    }
}
