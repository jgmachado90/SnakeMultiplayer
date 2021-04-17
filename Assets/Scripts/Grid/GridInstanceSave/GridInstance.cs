using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Grid/Instance", fileName = "GridInstance")]
public class GridInstance : ScriptableObject
{
    [SerializeField] private GridInfo _gridInfo;

    private List<List<EntitySaveInfo>> _everySnake = new List<List<EntitySaveInfo>>();
    public List<List<EntitySaveInfo>> EverySnake { get { return _everySnake; } set { _everySnake = value; } }


    private List<EntitySaveInfo> _everyCollectable = new List<EntitySaveInfo>();
    public List<EntitySaveInfo> EveryCollectable { get { return _everyCollectable; } set { _everyCollectable = value; } }

    public void SaveGridInstance()
    {
        EverySnake.Clear();
        List<Entity> entities = _gridInfo.GetEntities();
        FindSnakes(entities);

        EveryCollectable.Clear();
        FindCollectables(entities);
    }

    private void FindCollectables(List<Entity> entities)
    {
        foreach (Entity e in entities)
        {
            ICollectable collectable = e.GetComponent<ICollectable>();
            if (collectable != null)
            {
                 AddThisCollectable(e);
            }
        }
    }

    private void AddThisCollectable(Entity collectableEntity)
    {
 
        int x = collectableEntity.currentGridCell.coordinate.x;
        int y = collectableEntity.currentGridCell.coordinate.y;
        GameObject collectablePrefab = collectableEntity.EntityInfo.EntityPrefab;
        Transform collectableParent = collectableEntity.transform.parent;

        EntitySaveInfo entityToSave = new EntitySaveInfo(x,y,collectablePrefab,collectableParent, false);

        EveryCollectable.Add(entityToSave);
    }

    private void FindSnakes(List<Entity> entities)
    {
        foreach (Entity e in entities)
        {
            SnakePart snakePart = e.GetComponent<SnakePart>();
            if (snakePart != null)
            {
                if (snakePart.IsHead)
                {
                    AddThisSnake(snakePart);
                }
            }
        }
    }

    private void AddThisSnake(SnakePart snakePart)
    {

        List<EntitySaveInfo> thisSnake = new List<EntitySaveInfo>();

        //adding the head
        int x = snakePart.currentGridCell.coordinate.x;
        int y = snakePart.currentGridCell.coordinate.y;
        GameObject snakePrefab = snakePart.EntityInfo.EntityPrefab;
        Transform snakePartTransform = snakePart.transform.parent;
        bool snakeHead = snakePart.IsHead;

        EntitySaveInfo entityToSave = new EntitySaveInfo(x, y, snakePrefab, snakePartTransform, snakeHead);

        thisSnake.Add(entityToSave);

        SnakePart snakeTail = (SnakePart)snakePart.Prox;

        int i = 0;
        while (snakeTail != snakePart)
        {
            x = snakeTail.currentGridCell.coordinate.x;
            y = snakeTail.currentGridCell.coordinate.y;
            snakePrefab = snakeTail.EntityInfo.EntityPrefab;
            snakePartTransform = snakeTail.transform.parent;
            snakeHead = snakeTail.IsHead;

            entityToSave = new EntitySaveInfo(x, y, snakePrefab, snakePartTransform, snakeHead);
            thisSnake.Add(entityToSave);

            snakeTail = (SnakePart)snakeTail.Prox;
            i++;
            if (i > 500)
                break;
        }
        EverySnake.Add(thisSnake);
    }
}

public struct EntitySaveInfo{

    public int x;
    public int y;
    public GameObject prefab;
    public Transform parent;
    public bool isHead;  

    public EntitySaveInfo(int x, int y, GameObject prefab, Transform parent, bool isHead)
    {
        this.x = x;
        this.y = y;
        this.prefab = prefab;
        this.parent = parent;
        this.isHead = isHead;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public static bool operator == (EntitySaveInfo e1, EntitySaveInfo e2)
    {
        return e1.Equals(e2);
    }

    public static bool operator != (EntitySaveInfo e1, EntitySaveInfo e2)
    {
        return !e1.Equals(e2);
    }

}

