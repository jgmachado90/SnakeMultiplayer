using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Snake/StartingBlocks", fileName = "SnakeStartingBlocks")]
public class SnakeStartingBlocks : ScriptableObject
{
    [SerializeField] private CollectableType _firstBlock;
    [SerializeField] private CollectableType _secondBlock;
    [SerializeField] private CollectableType _thirdBlock;

    public CollectableType FirstBlock { get { return _firstBlock; } }
    public CollectableType SecondBlock { get { return _secondBlock; } }
    public CollectableType ThirdBlock { get { return _thirdBlock; } }

}
