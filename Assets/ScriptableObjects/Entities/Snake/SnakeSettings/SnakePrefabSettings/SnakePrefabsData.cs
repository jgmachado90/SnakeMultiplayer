using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SnakeSettings/PrefabData", fileName = "SnakePrefabData")]
public class SnakePrefabsData : ScriptableObject
{
    [SerializeField] private List<GameObject> _snakePrefab;
    public List<GameObject> SnakePrefab { get { return _snakePrefab; } }

    [SerializeField] private GameObject _tailBlockPrefab;
    public GameObject TailBlockPrefab { get { return _tailBlockPrefab; } }

}
