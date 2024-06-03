using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    private Transform firePoint; // Fire point
    public GameObject bulletPrefab; 
    // Start is called before the first frame update
    public float coneAngle = 45.0f;

    public float fireRate = 0.05f; // Time between each shot
    public float speed = 2.0f; // Speed of bullet
    public int bulletCount = 1; // Number of bullets to shoot in a cone
    public float timer  = 0f;
    


    void Start()
    {
        firePoint = transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            FireCone();
            timer = 0f;
        } 
    }

    void FireCone()
    {
        float angleStep = coneAngle / (bulletCount - 1);
        float startAngle = -coneAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            float bulletDirX = firePoint.position.x + Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            float bulletDirY = firePoint.position.y + Mathf.Cos(currentAngle * Mathf.Deg2Rad);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0);
            Vector3 bulletDir = (bulletMoveVector - firePoint.position).normalized;

            GameObject bullet = ObjectPool.Instance.GetPooledObj();

            bullet.GetComponent<Bullet>().isEnemy = true;

            if (bullet != null)
            {
                bullet.transform.position = bulletDir;
                bullet.SetActive(true);
            }
        }
    }
}
