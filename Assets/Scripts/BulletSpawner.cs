using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;

    public float spawnCooldown;
    float timer;
    public float bulletSpeed;
    public Vector3 bulletVelocity;

    float[] rotations;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnCooldown;
        rotations = new float[numberOfBullets];
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            spawnBullets();
            timer = spawnCooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] BulletDistributions()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            var fraction = (float)i / (float)numberOfBullets;
            var diff = maxRotation - minRotation;
            var fractionDiff = fraction * diff;
            rotations[i] = fractionDiff * minRotation;
        }
        
        return rotations;
    }

    public GameObject[] spawnBullets()
    {
        GameObject[]  spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            spawnedBullets[i] = Instantiate(bulletPrefab, transform);

            Bullet b = spawnedBullets[i].GetComponent(typeof(Bullet)) as Bullet;
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
        }
        return spawnedBullets;
    }
}
