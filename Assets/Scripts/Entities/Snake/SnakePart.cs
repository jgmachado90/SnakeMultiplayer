using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : Entity
{
    [SerializeField] private bool _isHead;
    [SerializeField] private Sprite headSprite;
    [SerializeField] private Sprite tailSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform _batteringRam;
    [SerializeField] private SnakeSettings _snakeSettings;


    public bool IsHead
    {
        get 
        {
            return _isHead;
        }
        set
        {
            if (value)
            {
                spriteRenderer.sprite = headSprite;
                if (_snakeSettings.HasBatteringRam)
                    _batteringRam.gameObject.SetActive(true);
                
            }
            else
            {
                spriteRenderer.sprite = tailSprite;
                _batteringRam.gameObject.SetActive(false);
                
            }
        }
    }

    public bool hasFood;

    private SnakePart _prox;

    public SnakePart Prox
    {
        get
        {
            return _prox;
        }
        set
        {
            _prox = value;
        }
    }

    private SnakePart _prev;

    public SnakePart Prev
    {
        get
        {
            return _prev;
        }
        set
        {
            _prev = value;
        }
    }

}
