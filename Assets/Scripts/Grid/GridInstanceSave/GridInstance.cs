using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Grid/Instance", fileName = "GridInstance")]
public class GridInstance : ScriptableObject
{
    [SerializeField] private GridInfo _gridInfo;

    private List<List<Tuple<int, int, GameObject, Transform, bool>>> _everySnake = new List<List<Tuple<int, int, GameObject, Transform, bool>>>();
    public List<List<Tuple<int, int, GameObject, Transform, bool>>> EverySnake { get { return _everySnake; } set { _everySnake = value; } }


    private List<Tuple<int, int, GameObject, Transform>> _everyCollectable = new List<Tuple<int, int, GameObject, Transform>>();
    public List<Tuple<int, int, GameObject, Transform>> EveryCollectable { get { return _everyCollectable; } set { _everyCollectable = value; } }

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

        Tuple<int, int, GameObject, Transform> collectable = new Tuple<int, int, GameObject, Transform>(x, y, collectablePrefab, collectableParent);

        EveryCollectable.Add(collectable);
    }

    private void FindSnakes(List<Entity> entities)
    {
        foreach (Entity e in entities)
        {
            SnakePart snakePart = e.GetComponent<SnakePart>();
            if (snakePart != null)
            {
                if (snakePart.isHead)
                {
                    AddThisSnake(snakePart);
                }
            }
        }
    }

    private void AddThisSnake(SnakePart snakePart)
    {

        List<Tuple<int, int, GameObject, Transform, bool>> thisSnake = new List<Tuple<int, int, GameObject, Transform, bool>>();
        //adding the head
        int x = snakePart.currentGridCell.coordinate.x;
        int y = snakePart.currentGridCell.coordinate.y;
        GameObject snakePrefab = snakePart.EntityInfo.EntityPrefab;
        Transform snakePartTransform = snakePart.transform.parent;
        bool snakeHead = snakePart.isHead;

        Tuple<int, int, GameObject, Transform, bool> entity = new Tuple<int, int, GameObject, Transform, bool>(x, y, snakePrefab, snakePartTransform, snakeHead);

        thisSnake.Add(entity);

        SnakePart snakeTail = (SnakePart)snakePart.Prox;

        int i = 0;
        while (snakeTail != snakePart)
        {
            x = snakeTail.currentGridCell.coordinate.x;
            y = snakeTail.currentGridCell.coordinate.y;
            snakePrefab = snakeTail.EntityInfo.EntityPrefab;
            snakePartTransform = snakeTail.transform.parent;
            snakeHead = snakeTail.isHead;

            entity = new Tuple<int, int, GameObject, Transform, bool>(x, y, snakePrefab, snakePartTransform, snakeHead);
            thisSnake.Add(entity);

            snakeTail = (SnakePart)snakeTail.Prox;
            i++;
            if (i > 500)
                break;
        }
        _everySnake.Add(thisSnake);
    }
}


