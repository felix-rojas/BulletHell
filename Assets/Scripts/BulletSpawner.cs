using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletBehaviors[] spawnBehaviors;
    public int pattern_index = 0;
    BulletBehaviors GetSpawnBehavior()
    {
        return spawnBehaviors[pattern_index];
    }

    float timer;

    float[] rotations;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetSpawnBehavior().spawnCooldown;
        rotations = new float[GetSpawnBehavior().numberOfBullets];
        BulletDistributions();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            spawnBullets();
            timer = GetSpawnBehavior().spawnCooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] BulletDistributions()
    {
        for (int i = 0; i < GetSpawnBehavior().numberOfBullets; i++)
        {
            var fraction = (float)i / (float)GetSpawnBehavior().numberOfBullets;
            var diff = GetSpawnBehavior().maxRotation - GetSpawnBehavior().minRotation;
            var fractionDiff = fraction * diff;
            rotations[i] = fractionDiff * GetSpawnBehavior().minRotation;
        }
        
        return rotations;
    }

    public GameObject[] spawnBullets()
    {
        GameObject[]  spawnedBullets = new GameObject[GetSpawnBehavior().numberOfBullets];
        for (int i = 0; i < GetSpawnBehavior().numberOfBullets; i++)
        {
            // stop instantiating, reuse pool instead
            //spawnedBullets[i] = Instantiate(bulletPrefab, transform);

            spawnedBullets[i] = ObjectPool.GetBulletInPool();

            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnBehavior().bulletPrefab, transform);
            }
            else 
            {
                spawnedBullets[i].transform.SetParent(transform);
                spawnedBullets[i].transform.localPosition = Vector3.zero;
            }

            Bullet b = spawnedBullets[i].GetComponent(typeof(Bullet)) as Bullet;
            
            b.rotation = rotations[i];
            b.speed = GetSpawnBehavior().bulletSpeed;
            b.velocity = GetSpawnBehavior().bulletVelocity;
            if (!GetSpawnBehavior().isParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }
        return spawnedBullets;
    }
}
