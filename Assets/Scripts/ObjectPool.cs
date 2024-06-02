using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public  GameObject prefab;
    public static List<GameObject> pooledObjects;
    public int poolSize = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate<GameObject>(prefab);
            b.SetActive(false);
            pooledObjects.Add(b);
        }
    }

    public static GameObject GetBulletInPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}