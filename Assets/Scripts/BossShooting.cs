using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10.0f;
    public int enemyLife = 30;

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0.0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObj();
        bullet.GetComponent<Bullet>().isEnemy = true;

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            enemyLife -= 1;
        }
    }

}
