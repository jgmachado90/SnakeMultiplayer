using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakesInputHandeler : MonoBehaviour
{
    [Header("DYNAMIC DATA")]
    [SerializeField] private PlayerInput _currentInput;
    public PlayerInput CurrentInput { get { return _currentInput; } set { _currentInput = value; } }

    [Header("EVENTS")]
    [SerializeField] private VoidEvent OnInstantiateNewSnake;
    [SerializeField] private VoidEvent OnChangeSnakePrefab;
    [SerializeField] private VoidEvent OnAssignInputKeys;
    [SerializeField] private VoidEvent OnPrepareToReceiveNewSnake;

    [Header("REFERENCES")]
    public InputHandeler inputHandeler;

    [Header("VARIABLES")]
    public float timePressedToAssignNewPlayer;

    

    private List<Tuple<KeyCode, KeyCode>> playerInputKeys = new List<Tuple<KeyCode, KeyCode>>();

    private float time;
    private float currentTimeToAssignNewPlayer;
    private float firstTimeToAssignNewPlayer;
   

    private void Start()
    {
        time = 0;

    }
    private void Update()
    {
        if (inputHandeler.ActiveInputs.Count == 2 && CurrentInput.MyInput == null)
        {
            SetCurrentInputKeys();
            StartPressingTimeCount();
        }

        else if (inputHandeler.ActiveInputs.Count == 2 && CurrentInput.MyInput != null)
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

        if (inputHandeler.ActiveInputs.Count < 2 && CurrentInput.MyInput != null)
        {
            if (Time.time >= firstTimeToAssignNewPlayer)
            {
                OnAssignInputKeys.Raise();
                OnPrepareToReceiveNewSnake.Raise();
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
            KeyCode firstKey = inputHandeler.ActiveInputs[0];
            KeyCode secondKey = inputHandeler.ActiveInputs[1];
            CurrentInput.MyInput = new Tuple<KeyCode, KeyCode>(firstKey, secondKey);
        }
    }
}
