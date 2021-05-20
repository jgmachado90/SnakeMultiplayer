using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeBlock : Entity
{
    [SerializeField] private bool _isHead;

    [SerializeField] public Sprite headSprite;
    [SerializeField] public Sprite tailSprite;
    [SerializeField] public Sprite engineSprite;
    [SerializeField] public Sprite batteringRamSprite;
    [SerializeField] public Sprite timeTravelSprite;


    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private Transform _batteringRam;
    public Transform BatteringRam {get {return _batteringRam;} }
    [SerializeField] private SnakeSettings _snakeSettings;

    [SerializeField] private CollectableType _blockType;

    public CollectableType BlockType
    {
        get
        {
            return _blockType;
        }
        set
        {
           switch (value)
           {
                case CollectableType.Food:
                    spriteRenderer.sprite = tailSprite;
                    break;
                case CollectableType.EnginePower:
                    spriteRenderer.sprite = engineSprite;
                    break;
                case CollectableType.BatteringRam:
                    spriteRenderer.sprite = batteringRamSprite;
                    break;
                case CollectableType.TimeTravel:
                    spriteRenderer.sprite = timeTravelSprite;
                    break;
           }
            _blockType = value;
        }
    }

    public bool IsHead
    {
        get 
        {
            return _isHead;
        }
        set
        {
            
            _isHead = value;
        }
    }

    [SerializeField] private bool _hasFood;
    public bool HasFood
    {
        get
        {
            return _hasFood;
        }
        set
        {
            if (value)
            {
                float scaleValue = _snakeSettings.SnakeScaleSettings.FeededScale.Value;
                transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                _hasFood = value;
            }
            else
            {
                float scaleValue = _snakeSettings.SnakeScaleSettings.StartScale.Value;
                transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                _hasFood = value;
            }     
        }
    }
}
