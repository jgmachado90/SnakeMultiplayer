using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Info", fileName = "EntityInfo")]
public class EntityInfo : ScriptableObject
{
    [SerializeField] private GameObject _entityPrefab;

    public GameObject EntityPrefab { get { return _entityPrefab; } }
}
