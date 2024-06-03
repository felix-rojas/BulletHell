using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Enemy enemy;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10.0f;

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

}
