using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public static List<GameObject> pooledObjects;
    public int poolSize = 100;
    private void Start()
    {
        pooledObjects = new List<GameObject>();
    }
    public static GameObject GetBulletInPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeSelf)
            {
                pooledObjects[i].GetComponent<Bullet>().ResetTimer();
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }
}