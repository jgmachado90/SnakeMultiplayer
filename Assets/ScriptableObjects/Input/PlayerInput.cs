using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PlayerInput/Input", fileName = "PlayerInput")]
public class PlayerInput : ScriptableObject
{
    [SerializeField] private Tuple<KeyCode,KeyCode> _myInput;
    
    public Tuple<KeyCode, KeyCode> MyInput { get { return _myInput; } set { _myInput = value; } }
}
