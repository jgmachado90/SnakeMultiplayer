using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [Header("SnakeInstanceVariables")]
   
    [SerializeField] private SnakeBlock _currentHead;
    [SerializeField] private List<SnakeBlock> _thisSnake;
    [SerializeField] private bool _canMove;
    private int _startingX;
    private int _startingY;
    private Color _snakeColor;

    public SnakeBlock CurrentHead { get { return _currentHead; } set { _currentHead = value; } }
    public List<SnakeBlock> ThisSnake { get { return _thisSnake; } set { _thisSnake = value; } }
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    public int StartingX { get { return _startingX; } set { _startingX = value; } }
    public int StartingY { get { return _startingY; } set { _startingY = value; } }
    public Color SnakeColor { get { return _snakeColor; } set { _snakeColor = value; } }

    [Header("Snake")]
    [SerializeField] private SnakeSettings _snakeSettings;
    public SnakeSettings SnakeSettings { set { _snakeSettings = value; } }

    [Header("Grid")]
    [SerializeField] private GridManager _gridManager;


    private ISnakeInput _snakeInput;
    private SnakeMover _snakeMover;
    private SnakeEater _snakeEater;

    
    public bool dead = false;


    private void Awake()
    {
        _canMove = false;
        _snakeInput = new ControllerInput();
        _snakeMover = new SnakeMover(_snakeInput, _gridManager, this);
        _snakeEater = new SnakeEater(_snakeSettings, this);
    }

    public void SetStartPosition(int x, int y)
    {
        StartingX = x;
        StartingY = y;
    }
    public void AssignSnakeInput(KeyCode keyLeft, KeyCode keyRight)
    {
        _snakeInput.LeftKey = keyLeft;
        _snakeInput.RightKey = keyRight;
    }

    public void RestartSnake()
    {
        //To do
    }


    IEnumerator TickCoroutine()
    {
        GridCell newSnakeBlockPosition = null;
        while (true)
        {
            if (CanMove)
            {
                float snakeMovementsPerSec = _snakeEater.GetSnakeCurrentSpeed();
                yield return new WaitForSeconds(snakeMovementsPerSec);

                bool willGrow = _snakeEater.HasFoodInTheLastPosition();
                if (willGrow)
                    newSnakeBlockPosition = ThisSnake.Last().currentGridCell;

                _snakeMover.Tick();



                if (willGrow)
                    Grow(newSnakeBlockPosition);
            }
            yield return null;
        }
    }

    public void OnGameStart()
    {
        StartCoroutine(TickCoroutine());
        _canMove = true;
    }

    private void Update()
    {
        _snakeInput.ReadInput();
    }

    internal void Die()
    {
        ReloadThisSnake();
    }

    private void ReloadThisSnake()
    {
        Debug.Log("Reload Snake By Death");
        SnakeInitializer snakeInitializer = GetComponent<SnakeInitializer>(); 
        snakeInitializer.ChangeSnake(snakeInitializer.SnakeStartingBlocks);

        _snakeInput.LookingDirection = Direction.Right;
        _snakeInput.MovingDirection = Direction.Right;
     
    }

    public void Feed(Entity collector, CollectableType collectedType)
    {
        _snakeEater._nextTailParts.Add(collectedType);
        SnakeBlock collectorSnakeBlock = (SnakeBlock)collector;
        collectorSnakeBlock.HasFood = true;
    }

    public void Grow(GridCell newBlockPosition)
    {
        GameObject tailPrefab = _snakeSettings.SnakePrefabSettings.TailPrefab;
        GameObject newTailGO = Instantiate(tailPrefab, transform);

        SnakeBlock newTail = newTailGO.GetComponent<SnakeBlock>();

        newTail.BlockType = _snakeEater.NextTailPartsPop();
        newTail.GetComponent<SpriteRenderer>().color = SnakeColor;
        SnakeBlock lastSnakePart = ThisSnake.Last();
        lastSnakePart.HasFood = false;

        newTail.currentGridCell = newBlockPosition;

        ThisSnake.Add(newTail);
    }


    internal void CutInThisBlock(SnakeBlock entityOcupating)
    {
        int indexToRemove = 0;
        int i = 0;
        foreach(SnakeBlock snakeBlock in ThisSnake)
        {
            if(snakeBlock == entityOcupating)
            {
                indexToRemove = i;
       
            }
            if(indexToRemove != 0)
                Destroy(snakeBlock.gameObject);

            i++;
        }

        ThisSnake.RemoveRange(indexToRemove, ThisSnake.Count - indexToRemove);
    }
}
