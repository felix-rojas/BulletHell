using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private readonly List<GameObject> BulletPool = new();
    private readonly int poolSize = 1000;

    public GameObject bulletPrefab;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate(bulletPrefab);
            b.SetActive(false);
            BulletPool.Add(b);
        }
    }

    public GameObject GetPooledObj()
    {
        for (int i = 0; i < BulletPool.Count; i++)
        {
            if (!BulletPool[i].activeInHierarchy)
            {
                return BulletPool[i];
            }
        }
        return null;
    }

    public int GetActiveInHierarchyCount()
    {
        int count = 0;
        foreach (GameObject bullet in BulletPool)
        {
            if (bullet.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}
