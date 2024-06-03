using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public int enemyLife = 30;
    public int numberOfBullets = 5; // Number of bullets in the cone
    public float coneAngle = 45.0f; // Total angle of the cone
    public int shootingPattern = 0;
    public float shootingPatternTime = 0.0f;
    public float patternSwitchTime = 10.0f;

    void Update()
    {
        shootingPatternTime += Time.deltaTime;
        if (shootingPatternTime >= patternSwitchTime)
        {
            shootingPattern = (shootingPattern + 1) % 3; // Switch to the next pattern
            shootingPatternTime = 0.0f; // Reset the timer
        }

        // Perform the movement based on the current pattern
        switch (shootingPattern)
        {
            case 0:
                Shoot();
                break;
            case 1:
                ShootCircles();
                break;
            case 2:
                ShootLine();
                break;
        }
    }

    void Shoot()
    {
        float angleStep = coneAngle / (numberOfBullets - 1);
        float startAngle = -coneAngle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetPooledObj();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().isEnemy = true;

                float angle = startAngle + angleStep * i;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.back;

                bullet.GetComponent <Bullet>().velocity = direction;
            }
        }
    }

    void ShootLine()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObj();
        bullet.GetComponent<Bullet>().isEnemy = true;

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
        }
    }

    void ShootCircles()
    {
        float angleStep = 360.0f / numberOfBullets;
        float angle = 0.0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetPooledObj();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().isEnemy = true;

                float bulletDirX = firePoint.position.x + Mathf.Sin(angle * Mathf.Deg2Rad);
                float bulletDirY = firePoint.position.y + Mathf.Cos(angle * Mathf.Deg2Rad);

                Vector3 bulletMoveDirection = new Vector3(bulletDirX, bulletDirY, 0f) - firePoint.position;
                bulletMoveDirection.Normalize();

                bullet.GetComponent<Bullet>().velocity = bulletMoveDirection;

                angle += angleStep;
            }
        }
    }
}
