﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/PrefabsSettings", fileName = "SnakePrefabsSettings")]
public class SnakePrefabsSettings : ScriptableObject
{
    [SerializeField] private GameObject _tailPrefab;
    public GameObject TailPrefab { get { return _tailPrefab; } }

}
